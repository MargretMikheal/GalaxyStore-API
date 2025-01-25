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

        public IGenericRepository<Warehouse> Warehouses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(context);
            Inventory = new GenericRepository<Inventory>(context);
            Items = new GenericRepository<Item>(context);
            Invoices = new GenericRepository<Invoice>(context);
            InvoiceItems = new GenericRepository<InvoiceItem>(context);
            Partners = new GenericRepository<Partner>(context);
            Warehouses = new GenericRepository<Warehouse>(context);
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
