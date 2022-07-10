namespace ECommerse.API.Search.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        public int Total { get; set; }

        public List<OrderItemModel> OderItems { get; set; }
    }
}
