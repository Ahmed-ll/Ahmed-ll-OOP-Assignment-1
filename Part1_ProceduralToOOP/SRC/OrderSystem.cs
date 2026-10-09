namespace SRC;

public class OrderSystem
{
    private readonly List<Customer> _customers = new List<Customer>();
    private readonly List<Product> _products = new List<Product>();
    private readonly List<Order> _orders = new List<Order>();

    public void AddCustomer(int id, string name, string email, string city, bool isVip)
    {
        if (_customers.Any(c => c.Id == id))
        {
            Console.WriteLine($"Customer with id {id} already exists.");
            return;
        }

        _customers.Add(new Customer(id, name, email, city, isVip));
    }

    public void AddProduct(int id, string name, decimal price, int stock)
    {
        if (_products.Any(p => p.Id == id))
        {
            Console.WriteLine($"Product with id {id} already exists.");
            return;
        }

        _products.Add(new Product(id, name, price, stock));
    }

    public void CreateOrder(int orderId,int customerId,string date)
    {
        if (_orders.Any(o => o.Id == orderId))
        {
            Console.WriteLine($"Order with id {orderId} already exists.");
            return;
        }

        Customer? customer =_customers.FirstOrDefault(c => c.Id == customerId);

        if (customer is null)
        {
            Console.WriteLine($"Customer with id {customerId} not found.");
            return;
        }

        _orders.Add(new Order(orderId, customer, date));
    }

    public void AddLineToOrder(int orderId, int productId, int quantity)
    {
        Order? order = _orders.FirstOrDefault(o => o.Id == orderId);

        if (order is null)
        {
            Console.WriteLine($"Order with id {orderId} not found.");
            return;
        }

        Product? product = _products.FirstOrDefault(p => p.Id == productId);

        if (product is null)
        {
            Console.WriteLine($"Product with id {productId} not found.");
            return;
        }

        if (!order.AddLine(product, quantity))
            Console.WriteLine($"ERROR: cannot add product #{productId}.");
    }

    public void MarkOrderPaid(int orderId)
    {
        Order? order =_orders.FirstOrDefault(o => o.Id == orderId);

        if (order is null)
        {
            Console.WriteLine($"Order with id {orderId} not found.");
            return;
        }

        if (!order.MarkAsPaid()) Console.WriteLine("ERROR: cannot pay an empty order.");
    }

    public decimal CalculatePaidSales()=> _orders.Where(o => o.IsPaid).Sum(o => o.CalculateTotal());   

    public void PrintCustomers()
    {
        foreach (Customer customer in _customers)
            Console.WriteLine($"{customer.Id}::{customer.Name}, Email: {customer.Email}, City: {customer.City}, VIP: {(customer.IsVip ? "yes" : "no")}");
    }

    public void PrintProducts()
    {
        foreach (Product product in _products)
            Console.WriteLine($"#{product.Id}::{product.Name}, Price = {product.Price}, Stock = {product.Stock}");
    }

    public void PrintAllOrders()
    {
        foreach (Order order in _orders)
            PrintOrder(order.Id);
    }

    public void PrintOrder(int orderId)
    {
        Order? order =_orders.FirstOrDefault(o => o.Id == orderId);

        if (order is null)
        {
            Console.WriteLine($"Order with id {orderId} not found.");
            return;
        }

        Console.WriteLine($"\n=== ORDER #{order.Id} ===");
        Console.WriteLine($"Date: {order.Date}");
        Console.WriteLine($"Customer: {order.Customer.Name} #{order.Customer.Id}");
        Console.WriteLine($"Paid: {(order.IsPaid ? "yes" : "no")}");

        Console.WriteLine("Products in order:");

        foreach (OrderLine line in order.Lines)
        {
            Console.WriteLine($"  - {line.Product.Name}, Quantity: {line.Quantity}, Price {line.Product.Price} => Total: {line.GetTotal()}");
        }

        Console.WriteLine($"Total: {order.CalculateTotal()}");
    }


}