using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Data.Repositories;
using GalaxyStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GalaxyStore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;



        public IGenericRepository<Product> Products { get; }

        public IGenericRepository<Inventory> Inventory { get; }

        public IGenericRepository<Item> Items { get; }

        public IGenericRepository<Invoice> Invoices { get; }

        public IGenericRepository<InvoiceItem> InvoiceItems { get; }
        public IGenericRepository<Partner> Partners { get; }
        public IGenericRepository<Customer> Customers { get; }

        public IGenericRepository<Warehouse> Warehouses { get; }

        public UnitOfWork(
             ApplicationDbContext context,
             IGenericRepository<Product> products,
             IGenericRepository<Inventory> inventory,
             IGenericRepository<Item> items,
             IGenericRepository<Invoice> invoices,
             IGenericRepository<InvoiceItem> invoiceItems,
             IGenericRepository<Partner> partners,
             IGenericRepository<Customer> customers,
             IGenericRepository<Warehouse> warehouses)
        {
            _context = context;
            Products = products;
            Inventory = inventory;
            Items = items;
            Invoices = invoices;
            InvoiceItems = invoiceItems;
            Partners = partners;
            Customers = customers;
            Warehouses = warehouses;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
