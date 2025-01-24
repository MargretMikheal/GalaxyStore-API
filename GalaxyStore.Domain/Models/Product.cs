namespace GalaxyStore.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string ProductPhoto { get; set; }
        public string Name { get; set; }
        public decimal CurrentPurchase { get; set; }
        public decimal SellingPrice { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }

        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
