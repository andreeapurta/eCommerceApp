namespace eShop.Business.Models
{
    public class OrderLineItem
    {
        public int? LineItemId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}