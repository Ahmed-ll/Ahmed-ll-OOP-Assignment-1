namespace SRC;

public sealed class OldInvoice
{
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string? CustomerEmail { get; }
    public string CustomerPhone { get; }

    public Address ShippingAddress { get; }
    public Address BillingAddress { get; }

    public DateTime OrderDate { get; }
    public PaymentMethod PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }

    public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;

    // Private: the only way to get an OldInvoice is through OldInvoice.Builder.
    private OldInvoice(
        int invoiceId,
        string customerName,
        string? customerEmail,
        string customerPhone,
        Address shippingAddress,
        Address billingAddress,
        PaymentMethod paymentMethod,
        string currency,
        decimal subTotal,
        decimal discountAmount,
        decimal taxAmount,
        DateTime orderDate)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        OrderDate = orderDate;
    }

    public sealed class Builder
    {
        // Mandatory (set once, in the constructor)
        private readonly int _invoiceId;
        private readonly string _customerName;
        private readonly string _customerPhone;
        private readonly Address _shippingAddress;
        private readonly PaymentMethod _paymentMethod;
        private readonly string _currency;
        private readonly decimal _subTotal;
        private readonly DateTime _orderDate;

        // Optional
        private string? _customerEmail;
        private Address? _billingAddress;
        private decimal _discountAmount;
        private decimal _taxAmount;

        public Builder(
            int invoiceId,
            string customerName,
            string customerPhone,
            Address shippingAddress,
            PaymentMethod paymentMethod,
            string currency,
            decimal subTotal,
            DateTime orderDate)
        {
            if (invoiceId <= 0)
                throw new ArgumentOutOfRangeException(nameof(invoiceId), "InvoiceId must be positive.");
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name is required.", nameof(customerName));
            if (string.IsNullOrWhiteSpace(customerPhone))
                throw new ArgumentException("Customer phone is required.", nameof(customerPhone));
            ArgumentNullException.ThrowIfNull(shippingAddress);
            if (!Enum.IsDefined(paymentMethod))
                throw new ArgumentException("Invalid payment method.", nameof(paymentMethod));
            if (currency is null || currency.Length != 3 || !currency.All(char.IsLetter))
                throw new ArgumentException("Currency must be a 3-letter code (e.g. USD).", nameof(currency));
            if (subTotal < 0)
                throw new ArgumentOutOfRangeException(nameof(subTotal), "SubTotal cannot be negative.");
            if (orderDate == default)
                throw new ArgumentException("Order date is required.", nameof(orderDate));

            _invoiceId = invoiceId;
            _customerName = customerName;
            _customerPhone = customerPhone;
            _shippingAddress = shippingAddress;
            _paymentMethod = paymentMethod;
            _currency = currency.ToUpperInvariant();
            _subTotal = subTotal;
            _orderDate = orderDate;
        }

        public Builder WithCustomerEmail(string customerEmail)
        {
            if (string.IsNullOrWhiteSpace(customerEmail) || !customerEmail.Contains("@"))
                throw new ArgumentException("A valid email is required.", nameof(customerEmail));

            _customerEmail = customerEmail;
            return this;
        }

        public Builder WithBillingAddress(Address billingAddress)
        {
            ArgumentNullException.ThrowIfNull(billingAddress);
            _billingAddress = billingAddress;
            return this;
        }

        public Builder WithDiscount(decimal discountAmount)
        {
            if (discountAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(discountAmount), "Discount cannot be negative.");

            _discountAmount = discountAmount;
            return this;
        }

        public Builder WithTax(decimal taxAmount)
        {
            if (taxAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(taxAmount), "Tax cannot be negative.");

            _taxAmount = taxAmount;
            return this;
        }

        public OldInvoice Build()
        {
            if (_discountAmount > _subTotal)
                throw new InvalidOperationException("Discount cannot be greater than SubTotal.");

            return new OldInvoice(
                invoiceId: _invoiceId,
                customerName: _customerName,
                customerEmail: _customerEmail,
                customerPhone: _customerPhone,
                shippingAddress: _shippingAddress,
                billingAddress: _billingAddress ?? _shippingAddress,
                paymentMethod: _paymentMethod,
                currency: _currency,
                subTotal: _subTotal,
                discountAmount: _discountAmount,
                taxAmount: _taxAmount,
                orderDate: _orderDate);
        }
    }
}
