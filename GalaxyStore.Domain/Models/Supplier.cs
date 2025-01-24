namespace GalaxyStore.Domain.Models
{
    public class Supplier: Partner
    {
        public string Image { get; set; }
        public string IdImagePath { get; set; }
    }
}
//{Year}{ProductSerialCode}{SequentialNumber}
//YearCode (3 digits) + ProductSerialCode (4 digits) + ItemSerial (5 digits)
