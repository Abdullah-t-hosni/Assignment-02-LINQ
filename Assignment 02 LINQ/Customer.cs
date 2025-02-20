namespace Assignment_02_LINQ
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new List<Order>();
    }

    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}
