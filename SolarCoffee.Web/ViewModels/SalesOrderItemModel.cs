namespace SolarCoffee.Web.ViewModels
{
    public class SalesOrderItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public ProductModel Product { get; set; }
    }
}