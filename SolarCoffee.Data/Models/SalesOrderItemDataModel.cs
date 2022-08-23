namespace SolarCoffee.Data.Models
{
    public class SalesOrderItemDataModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public ProductDataModel Product { get; set; }
    }
}