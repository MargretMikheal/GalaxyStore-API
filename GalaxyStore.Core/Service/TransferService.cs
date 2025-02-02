using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.TransferDtos;


namespace GalaxyStore.Core.Service
{
    public class TransferService : ITransferService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<List<string>>> TransferToStoreAsync(TransferToStoreDto dto)
        {
            var response = new ServiceResponse<List<string>>();

            var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(i =>
                i.ProductId == dto.ProductId && i.WarehouseId == dto.WarehouseId);

            if (inventory == null)
            {
                return new ServiceResponse<List<string>>
                {
                    Success = false,
                    Message = "No inventory record found for the given warehouse and product."
                };
            }

            if (inventory.NumOfProductInStock < dto.Quantity)
            {
                return new ServiceResponse<List<string>>
                {
                    Success = false,
                    Message = "Not enough products in stock to transfer."
                };
            }

            var items = await _unitOfWork.Items.FindAsync(i =>
                i.ProductId == dto.ProductId && i.IsInStock);

            if (items.Count() < dto.Quantity)
            {
                return new ServiceResponse<List<string>>
                {
                    Success = false,
                    Message = "Insufficient stock available."
                };
            }

            var selectedItems = items.Take(dto.Quantity).ToList();

            foreach (var item in selectedItems)
            {
                item.IsInStock = false;
                _unitOfWork.Items.Update(item);
            }

            inventory.NumOfProductInStock -= dto.Quantity;
            inventory.NumOfProductInStore += dto.Quantity;
            _unitOfWork.Inventory.Update(inventory);

            await _unitOfWork.CompleteAsync();

            response.Success = true;
            response.Data = selectedItems.Select(i => i.Barcode).ToList();
            response.Message = "Products transferred to store successfully.";
            return response;
        }

        public async Task<ServiceResponse<string>> TransferToStockAsync(TransferToStockDto dto)
        {
            var response = new ServiceResponse<string>();

            if (dto.Barcodes == null || !dto.Barcodes.Any())
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "No barcodes provided for transfer."
                };
            }

            foreach (var barcode in dto.Barcodes)
            {
                var item = await _unitOfWork.Items.FirstOrDefaultAsync(i => i.Barcode == barcode);

                if (item == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = $"Item with barcode {barcode} not found."
                    };
                }

                item.IsInStock = true;
                _unitOfWork.Items.Update(item);
            }

            var groupedItems = dto.Barcodes.GroupBy(b =>
                _unitOfWork.Items.FirstOrDefaultAsync(i => i.Barcode == b).Result?.ProductId);

            foreach (var group in groupedItems)
            {
                var productId = group.Key;
                var quantity = group.Count();

                var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(i => i.ProductId == productId);

                if (inventory != null)
                {
                    inventory.NumOfProductInStock += quantity;
                    inventory.NumOfProductInStore -= quantity;
                    _unitOfWork.Inventory.Update(inventory);
                }
            }

            await _unitOfWork.CompleteAsync();

            response.Success = true;
            response.Message = "Products transferred to stock successfully.";
            return response;
        }
    }
}
