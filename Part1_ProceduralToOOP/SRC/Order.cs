namespace SRC;

public class Order
{
    private readonly List<OrderLine> _lines = new();
    public IReadOnlyList<OrderLine> Lines => _lines;

    public int Id { get; }
    public string Date { get; }
    public bool IsPaid { get; private set; }
    public Customer Customer { get; }


    public Order(int id, Customer customer, string date)
    {
        Id = id;
        Customer = customer;
        Date = date;
    }

    public bool AddLine(Product product, int quantity)
    {
        if (IsPaid)
            return false;

        _lines.Add(new OrderLine(product, quantity));
        return true;
    }

    public decimal CalculateTotal()
    {
        decimal total = _lines.Sum(line => line.GetTotal());

        if (Customer.IsVip)
            total *= 0.90m;

        return total;
    }

    public bool MarkAsPaid()
    {
        if (_lines.Count == 0)
            return false;

        IsPaid = true;
        return true;
    }
}