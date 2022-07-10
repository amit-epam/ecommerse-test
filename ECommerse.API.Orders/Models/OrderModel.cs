namespace ECommerse.API.Orders.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public int CustomerID { get; set; }
        //public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Total { get; set; }

        public List<OrderItemModel> OderItems { get; set; }
    }
}
