namespace SRC;

public sealed class Order
{
    public DateTime OrderDate { get; }
    public PaymentMethod PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;

    private Order(
        DateTime date,
        PaymentMethod paymentMethod,
        string currency,
        decimal subtotal,
        decimal discount,
        decimal tax)
    {
        if (date == default)
            throw new ArgumentException("Order date is required.",nameof(date));

        if (!Enum.IsDefined(paymentMethod))
            throw new ArgumentException("Invalid payment method.",nameof(paymentMethod));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.",nameof(currency));

        if (subtotal < 0)
            throw new ArgumentOutOfRangeException(nameof(subtotal),"SubTotal cannot be negative.");

        if (discount < 0)
            throw new ArgumentOutOfRangeException(nameof(discount),"Discount cannot be negative.");

        if (tax < 0)
            throw new ArgumentOutOfRangeException(nameof(tax),"Tax cannot be negative.");

        if (discount > subtotal)
            throw new ArgumentException("Discount cannot be greater than SubTotal.",nameof(discount));

        OrderDate = date;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subtotal;
        DiscountAmount = discount;
        TaxAmount = tax;
    }

    public sealed class Builder
    {
        private readonly DateTime _orderDate;
        private readonly PaymentMethod _paymentMethod;
        private readonly string _currency;
        private readonly decimal _subTotal;

        private decimal _discountAmount;
        private decimal _taxAmount;

        public Builder(
            DateTime date,
            PaymentMethod paymentMethod,
            string currency,
            decimal subTotal)
        {
            _orderDate = date;
            _paymentMethod = paymentMethod;
            _currency = currency;
            _subTotal = subTotal;
        }

        public Builder SetDiscount(decimal discount)
        {
            _discountAmount = discount;
            return this;
        }

        public Builder SetTax(decimal tax)
        {
            _taxAmount = tax;
            return this;
        }

        public Order Build()
        {
            return new Order(
                _orderDate,
                _paymentMethod,
                _currency,
                _subTotal,
                _discountAmount,
                _taxAmount);
        }
    }
}