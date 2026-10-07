using SRC;

class Program
{
    static void Main()
    {
        var market = new OrderSystem();

        // Add Customers
        market.AddCustomer(1, "Ahmed", "Ahmed@gmail.com", "Cairo", true);
        market.AddCustomer(2, "Omar", "omar@example.com", "Alexandria", false);

        // Add Products
        market.AddProduct(101, "USB Cable", 50m, 100);
        market.AddProduct(102, "Wireless Mouse", 250m, 40);

        // Create Order
        market.CreateOrder(1001, 1, "2026-09-15");

        // Add products to order
        market.AddLineToOrder(1001, 101, 2);
        market.AddLineToOrder(1001, 102, 1);

        // Pay order
        market.MarkOrderPaid(1001);

        // Print everything
        Console.WriteLine("=== CUSTOMERS ===");
        market.PrintCustomers();

        Console.WriteLine("\n=== PRODUCTS ===");
        market.PrintProducts();

        Console.WriteLine("\n=== ORDER ===");
        market.PrintOrder(1001);

        // Paid sales
        Console.WriteLine($"\nPaid Sales: {market.CalculatePaidSales():F2}");
    }
}