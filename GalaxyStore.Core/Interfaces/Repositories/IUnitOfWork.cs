using GalaxyStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Inventory> Inventory { get; }
        IGenericRepository<Item> Items { get; }
        IGenericRepository<Invoice> Invoices { get; }
        IGenericRepository<InvoiceItem> InvoiceItems { get; }
       IGenericRepository<Partner> Partners { get; } 
        IGenericRepository<Warehouse> Warehouses { get; }
        Task<int> CompleteAsync();
    }
}
