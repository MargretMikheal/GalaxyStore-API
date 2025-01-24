namespace GalaxyStore.Domain.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int NumOfProduct { get; set; }
    }
}
