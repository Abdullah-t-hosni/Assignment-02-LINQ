namespace Assignment_02_LINQ
{
    static class Program
    {
        static void Main(string[] args)
        {
            // List of products
            var products = new List<Product>
                {
                    new Product { ID = 1, Name = "Product A", Category = "Category 1", Price = 10, UnitsInStock = 5 },
                    new Product { ID = 2, Name = "Product B", Category = "Category 1", Price = 20, UnitsInStock = 0 },
                    new Product { ID = 3, Name = "Product C", Category = "Category 2", Price = 15, UnitsInStock = 10 },
                    new Product { ID = 4, Name = "Product D", Category = "Category 2", Price = 25, UnitsInStock = 7 },
                    new Product { ID = 5, Name = "Product E", Category = "Category 3", Price = 30, UnitsInStock = 3 }
                };

            // List of customers
            var customers = new List<Customer>
                {
                    new Customer { ID = 1, Name = "Customer X", State = "Washington" },
                    new Customer { ID = 2, Name = "Customer Y", State = "New York" },
                    new Customer { ID = 3, Name = "Customer Z", State = "Washington" }
                };

            // Adding orders to customers
            customers[0].Orders.Add(new Order { OrderID = 1, OrderDate = DateTime.Now, Total = 100 });
            customers[0].Orders.Add(new Order { OrderID = 2, OrderDate = DateTime.Now, Total = 200 });
            customers[2].Orders.Add(new Order { OrderID = 3, OrderDate = DateTime.Now, Total = 150 });

            // Execute LINQ operations
            LINQOperations.AggregateOperators(products);
            LINQOperations.SetOperators(products, customers);
            LINQOperations.PartitioningOperators(customers);
            LINQOperations.Quantifiers(products);
            LINQOperations.GroupingOperators();
        }
    }
}


