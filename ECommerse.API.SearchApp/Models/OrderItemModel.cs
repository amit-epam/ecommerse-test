namespace ECommerse.API.SearchApp.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

    }
}
