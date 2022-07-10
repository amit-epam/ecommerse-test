namespace ECommerse.API.Orders.Db
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Total { get; set; }

    }
}
