namespace Assignment_02_LINQ
{
    public static class LINQOperations
    {
        public static void AggregateOperators(List<Product> products)
        {
            // 1. Total units in stock for each category
            var totalUnitsInStock = products
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, TotalUnits = g.Sum(p => p.UnitsInStock) });

            Console.WriteLine("Total Units in Stock per Category:");
            foreach (var item in totalUnitsInStock)
            {
                Console.WriteLine($"{item.Category}: {item.TotalUnits}");
            }

            // 2. Cheapest price in each category
            var cheapestPricePerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, CheapestPrice = g.Min(p => p.Price) });

            Console.WriteLine("\nCheapest Price per Category:");
            foreach (var item in cheapestPricePerCategory)
            {
                Console.WriteLine($"{item.Category}: {item.CheapestPrice}");
            }

            // 3. Products with the cheapest price in each category (using Let)
            var cheapestProducts = from p in products
                                   group p by p.Category into g
                                   let minPrice = g.Min(p => p.Price)
                                   select new { Category = g.Key, CheapestProduct = g.First(p => p.Price == minPrice) };

            Console.WriteLine("\nCheapest Products per Category:");
            foreach (var item in cheapestProducts)
            {
                Console.WriteLine($"{item.Category}: {item.CheapestProduct.Name} (${item.CheapestProduct.Price})");
            }

            // 4. Most expensive price in each category
            var mostExpensivePricePerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MostExpensivePrice = g.Max(p => p.Price) });

            Console.WriteLine("\nMost Expensive Price per Category:");
            foreach (var item in mostExpensivePricePerCategory)
            {
                Console.WriteLine($"{item.Category}: {item.MostExpensivePrice}");
            }

            // 5. Products with the most expensive price in each category
            var mostExpensiveProducts = from p in products
                                        group p by p.Category into g
                                        let maxPrice = g.Max(p => p.Price)
                                        select new { Category = g.Key, MostExpensiveProduct = g.First(p => p.Price == maxPrice) };

            Console.WriteLine("\nMost Expensive Products per Category:");
            foreach (var item in mostExpensiveProducts)
            {
                Console.WriteLine($"{item.Category}: {item.MostExpensiveProduct.Name} (${item.MostExpensiveProduct.Price})");
            }

            // 6. Average price of each category's products
            var averagePricePerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.Price) });

            Console.WriteLine("\nAverage Price per Category:");
            foreach (var item in averagePricePerCategory)
            {
                Console.WriteLine($"{item.Category}: {item.AveragePrice}");
            }
        }

        // Set Operators
        public static void SetOperators(List<Product> products, List<Customer> customers)
        {
            // 1. Unique Category names
            var uniqueCategories = products.Select(p => p.Category).Distinct();
            Console.WriteLine("\nUnique Categories:");
            foreach (var category in uniqueCategories)
            {
                Console.WriteLine(category);
            }

            // 2. Unique first letters from product and customer names
            var uniqueFirstLetters = products.Select(p => p.Name[0])
                                              .Union(customers.Select(c => c.Name[0]))
                                              .Distinct();
            Console.WriteLine("\nUnique First Letters:");
            foreach (var letter in uniqueFirstLetters)
            {
                Console.WriteLine(letter);
            }

            // 3. Common first letters from product and customer names
            var commonFirstLetters = products.Select(p => p.Name[0])
                                             .Intersect(customers.Select(c => c.Name[0]));
            Console.WriteLine("\nCommon First Letters:");
            foreach (var letter in commonFirstLetters)
            {
                Console.WriteLine(letter);
            }

            // 4. First letters of product names not in customer names
            var uniqueProductFirstLetters = products.Select(p => p.Name[0])
                                                    .Except(customers.Select(c => c.Name[0]));
            Console.WriteLine("\nUnique Product First Letters:");
            foreach (var letter in uniqueProductFirstLetters)
            {
                Console.WriteLine(letter);
            }

            // 5. Last three characters of customer and product names
            var lastThreeChars = products.Select(p => p.Name.Substring(Math.Max(0, p.Name.Length - 3)))
                                      .Concat(customers.Select(c => c.Name.Substring(Math.Max(0, c.Name.Length - 3))));
            Console.WriteLine("\nLast Three Characters:");
            foreach (var chars in lastThreeChars)
            {
                Console.WriteLine(chars);
            }
        }

        // Partitioning Operators
        public static void PartitioningOperators(List<Customer> customers)
        {
            // 1. First 3 orders from customers in Washington
            var firstThreeOrders = customers.Where(c => c.State == "Washington")
                                            .SelectMany(c => c.Orders)
                                            .Take(3);
            Console.WriteLine("\nFirst 3 Orders from Washington:");
            foreach (var order in firstThreeOrders)
            {
                Console.WriteLine($"Order ID: {order.OrderID}, Total: {order.Total}");
            }

            // 2. All but the first 2 orders from customers in Washington
            var allButFirstTwoOrders = customers.Where(c => c.State == "Washington")
                                                .SelectMany(c => c.Orders)
                                                .Skip(2);
            Console.WriteLine("\nAll but the first 2 Orders from Washington:");
            foreach (var order in allButFirstTwoOrders)
            {
                Console.WriteLine($"Order ID: {order.OrderID}, Total: {order.Total}");
            }
        }

        // Quantifiers
        public static void Quantifiers(List<Product> products)
        {
            // 1. Check if any product is out of stock
            var anyOutOfStock = products.Any(p => p.UnitsInStock == 0);
            Console.WriteLine($"\nAny product out of stock: {anyOutOfStock}");

            // 2. Categories with at least one product out of stock
            var outOfStockCategories = products
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0))
                .Select(g => g.Key);
            Console.WriteLine("\nCategories with at least one product out of stock:");
            foreach (var category in outOfStockCategories)
            {
                Console.WriteLine(category);
            }

            // 3. Categories with all products in stock
            var allInStockCategories = products
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0))
                .Select(g => g.Key);
            Console.WriteLine("\nCategories with all products in stock:");
            foreach (var category in allInStockCategories)
            {
                Console.WriteLine(category);
            }
        }

        // Grouping Operators
        public static void GroupingOperators()
        {
            // 1. Group numbers by remainder when divided by 5
            List<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var groupedByRemainder = numbers.GroupBy(n => n % 5);
            Console.WriteLine("\nNumbers grouped by remainder when divided by 5:");
            foreach (var group in groupedByRemainder)
            {
                Console.WriteLine($"Remainder {group.Key}: {string.Join(", ", group)}");
            }

            // 2. Group words by their first letter
            string[] words = { "apple", "banana", "cherry", "date", "elderberry" };
            var groupedByFirstLetter = words.GroupBy(word => word[0]);
            Console.WriteLine("\nWords grouped by first letter:");
            foreach (var group in groupedByFirstLetter)
            {
                Console.WriteLine($"First letter '{group.Key}': {string.Join(", ", group)}");
            }
        }
    }
}
