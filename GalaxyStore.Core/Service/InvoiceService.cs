using GalaxyStore.Core.Helper;
using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.InvoiceDtos;
using GalaxyStore.Domain.Enums;
using GalaxyStore.Domain.Models;
using Mapster;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace GalaxyStore.Core.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BarcodeGenerator _barcodeGenerator;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _barcodeGenerator = new BarcodeGenerator(unitOfWork);
        }

        public async Task<ServiceResponse<int>> CreateSupplierInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            var response = new ServiceResponse<int>();

            if (invoiceDto == null || invoiceDto.InvoiceItems == null || !invoiceDto.InvoiceItems.Any())
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Invalid invoice data."
                };
            }

            var supplier = await _unitOfWork.Partners
                .FirstOrDefaultAsync(p => p is Supplier && p.Id == invoiceDto.SupplierId);
            if (supplier == null)
            {
                response.Success = false;
                response.Message = "Supplier not found.";
                return response;
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var invoice = invoiceDto.Adapt<Invoice>();
                    invoice.Type = InvoiceType.Buying;
                    invoice.Partner = supplier;
                    invoice.CreationDate = DateTime.UtcNow;

                    await _unitOfWork.Invoices.AddAsync(invoice);

                    foreach (var itemDto in invoiceDto.InvoiceItems)
                    {
                        var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
                        if (product == null)
                        {
                            response.Success = false;
                            response.Message = $"Product with ID {itemDto.ProductId} not found.";
                            return response;
                        }
                        product.CurrentPurchase = itemDto.ItemPrice;

                        var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(itemDto.WarehouseId);
                        if (warehouse == null)
                        {
                            response.Success = false;
                            response.Message = $"Warehouse with ID {itemDto.WarehouseId} not found.";
                            return response;
                        }

                        var invoiceItem = itemDto.Adapt<InvoiceItem>();
                        invoiceItem.Total = itemDto.Quantity * itemDto.ItemPrice;
                        invoice.InvoiceItems.Add(invoiceItem);
                        await _unitOfWork.InvoiceItems.AddAsync(invoiceItem);

                        var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(i =>
                            i.ProductId == itemDto.ProductId && i.WarehouseId == itemDto.WarehouseId);

                        if (inventory != null)
                        {
                            inventory.NumOfProductInStock += itemDto.Quantity;
                            _unitOfWork.Inventory.Update(inventory);
                        }
                        else
                        {
                            await _unitOfWork.Inventory.AddAsync(new Inventory
                            {
                                ProductId = itemDto.ProductId,
                                WarehouseId = itemDto.WarehouseId,
                                NumOfProductInStock = itemDto.Quantity,
                                NumOfProductInStore = 0
                            });
                        }

                        for (int i = 0; i < itemDto.Quantity; i++)
                        {
                            string barcode = await _barcodeGenerator.GenerateBarcodeAsync(itemDto.ProductId);
                            var newItem = new Item
                            {
                                ProductId = itemDto.ProductId,
                                SupplierId = invoice.PartnerId,
                                IsInStock = true,
                                Barcode = barcode
                            };
                            await _unitOfWork.Items.AddAsync(newItem);
                        }
                    }

                    invoice.TotalPay = invoice.InvoiceItems.Sum(ii => ii.Total);

                    supplier.LastTransactionDate = DateTime.UtcNow;
                    _unitOfWork.Partners.Update(supplier);

                    await _unitOfWork.CompleteAsync();
                    transaction.Complete();

                    response.Data = invoice.Id;
                    response.Success = true;
                    response.Message = "Invoice created successfully.";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = $"An error occurred: {ex.Message}";
                }
            }

            return response;
        }

        public async Task<ServiceResponse<List<LatestSupplierTransactionDto>>> GetLatestSupplierTransactionsAsync()
        {
            var response = new ServiceResponse<List<LatestSupplierTransactionDto>>();

            var invoices = await _unitOfWork.Invoices.GetAllAsync(
                i => i.Type == InvoiceType.Buying,
                i => i.InvoiceItems,
                i => i.Partner);

            if (!invoices.Any())
            {
                response.Success = false;
                response.Message = "No supplier transactions found.";
                return response;
            }

            var latestInvoices = invoices.OrderByDescending(i => i.CreationDate)
                                         .Take(20)
                                         .ToList();

            var transactions = latestInvoices.Select(inv => new LatestSupplierTransactionDto
            {
                SupplierName = inv.Partner.Name,
                TotalPay = inv.TotalPay,
                Date = inv.CreationDate
            }).ToList();

            response.Data = transactions;
            response.Success = true;
            response.Message = "Latest supplier transactions retrieved successfully.";
            return response;
        }

        public async Task<ServiceResponse<int>> CreateCustomerInvoiceAsync(CreateCustomerInvoiceDto invoiceDto)
        {
            var response = new ServiceResponse<int>();

            if (invoiceDto == null || invoiceDto.Items == null || !invoiceDto.Items.Any())
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Invalid invoice data."
                };
            }

            var customer = await _unitOfWork.Partners.FirstOrDefaultAsync(p => p is Customer && p.Id == invoiceDto.CustomerId);
            if (customer == null)
            {
                response.Success = false;
                response.Message = "Customer not found.";
                return response;
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var invoice = new Invoice
                    {
                        Type = InvoiceType.Selling,
                        PartnerId = invoiceDto.CustomerId,
                        CreationDate = DateTime.UtcNow,
                        InvoiceItems = new List<InvoiceItem>()
                    };
                    await _unitOfWork.Invoices.AddAsync(invoice);

                    foreach (var itemDto in invoiceDto.Items)
                    {
                        var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
                        if (product == null)
                        {
                            response.Success = false;
                            response.Message = $"Product with ID {itemDto.ProductId} not found.";
                            return response;
                        }

                        var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(invoiceDto.WarhouseId);
                        if (warehouse == null)
                        {
                            response.Success = false;
                            response.Message = $"Warehouse with ID {invoiceDto.WarhouseId} not found.";
                            return response;
                        }

                        var invoiceItem = new InvoiceItem
                        {
                            ProductId = itemDto.ProductId,
                            Quantity = itemDto.Barcodes.Count,
                            ItemPrice = product.SellingPrice,
                            Total = itemDto.Barcodes.Count * product.SellingPrice,
                            WarehouseId = invoiceDto.WarhouseId
                        };

                        await _unitOfWork.InvoiceItems.AddAsync(invoiceItem);
                        invoice.InvoiceItems.Add(invoiceItem);

                        var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(i => i.ProductId == itemDto.ProductId && i.WarehouseId == invoiceDto.WarhouseId);
                        if (inventory != null && inventory.NumOfProductInStore >= itemDto.Barcodes.Count)
                        {
                            inventory.NumOfProductInStore -= itemDto.Barcodes.Count;
                            _unitOfWork.Inventory.Update(inventory);
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = $"Insufficient stock for product ID {itemDto.ProductId} in warehouse ID {invoiceDto.WarhouseId}.";
                            return response;
                        }

                        foreach (var barcode in itemDto.Barcodes)
                        {
                            var item = await _unitOfWork.Items.FirstOrDefaultAsync(i => i.Barcode == barcode);
                            if (item != null)
                            {
                                item.IsDeleted = true;
                                item.DeletedDate = DateTime.UtcNow;
                                _unitOfWork.Items.Update(item);
                            }
                        }
                    }

                    invoice.TotalPay = invoice.InvoiceItems.Sum(ii => ii.Total);
                    customer.LastTransactionDate = DateTime.UtcNow;
                    _unitOfWork.Partners.Update(customer);
                    await _unitOfWork.CompleteAsync();
                    transaction.Complete();

                    response.Data = invoice.Id;
                    response.Success = true;
                    response.Message = "Customer invoice created successfully.";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = $"An error occurred: {ex.Message}";
                }
            }

            return response;
        }


    }
}
