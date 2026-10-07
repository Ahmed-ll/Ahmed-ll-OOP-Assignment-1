namespace SRC;

public sealed class NewInvoice
{
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string? CustomerEmail { get; }
    public string CustomerPhone { get; }
    public Address ShippingAddress { get; }
    public Address BillingAddress { get; }
    public Order OrderPaymentInfo { get; }

    private NewInvoice(
      int invoiceId,
      string customerName,
      string? customerEmail,
      string customerPhone,
      Address shippingAddress, Address billingAddress, Order orderPaymentInfo
      )
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        OrderPaymentInfo = orderPaymentInfo;
    }

    public sealed class Builder
    {
        // Mandatory (set once, in the constructor)
        private readonly int _invoiceId;
        private readonly string _customerName;
        private readonly string _customerPhone;
        private readonly Address _shippingAddress;
        private readonly Order _orderPaymentInfo;

        // Optional (set via methods)
        private string? _customerEmail;
        private Address? _billingAddress;

        public Builder
         (int invoiceId,
           string customerName,
           string customerPhone,
           Address shippingAddress,
           Order orderPaymentInfo)
        {
            if (invoiceId <= 0)
                throw new ArgumentOutOfRangeException("InvoiceId must be positive.");
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name is required.");
            if (string.IsNullOrWhiteSpace(customerPhone))
                throw new ArgumentException("Customer phone is required.");
            if (shippingAddress is null)
                throw new ArgumentNullException(nameof(shippingAddress));
            if (orderPaymentInfo is null)
                throw new ArgumentNullException(nameof(orderPaymentInfo));

            _invoiceId = invoiceId;
            _customerName = customerName;
            _customerPhone = customerPhone;
            _shippingAddress = shippingAddress;
            _orderPaymentInfo = orderPaymentInfo;
        }

        public Builder SetCustomerEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("A valid email is required.");

            _customerEmail = email;
            return this;
        }

        public Builder SetBillingAddress(Address billingAddress)
        {
            if (billingAddress is null)
                throw new ArgumentNullException(nameof(billingAddress));

            _billingAddress = billingAddress;
            return this;
        }

        public NewInvoice Build()
            => new NewInvoice(_invoiceId, _customerName, _customerEmail, _customerPhone, _shippingAddress, _billingAddress ?? _shippingAddress, _orderPaymentInfo);
    }
}