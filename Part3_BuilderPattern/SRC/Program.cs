namespace SRC;

public class Program
{
    static void Main(string[] args)
    {
        // Task 3.2: Single builder

        var oldInvoice = new OldInvoice.Builder(
                invoiceId: 1,
                customerName: "Ahmed",
                customerPhone: "01019849499",
                shippingAddress: new Address.Builder("10 ElTahrir", "Cairo", "Egypt").Build(),
                paymentMethod: PaymentMethod.Visa,
                currency: "EGP",
                subTotal: 1000m,
                orderDate: DateTime.Today)
            .WithCustomerEmail("ahmed@example.com")
            .WithBillingAddress(new Address.Builder("5 Pyramids", "Giza", "Egypt").Build())
            .WithDiscount(100m)
            .WithTax(126m)
            .Build();

        // Task 3.3: Multiple builders 

        var shipping = new Address.Builder("10 Tahrir St", "Cairo", "Egypt")
            .SetState("Cairo")
            .SetZipCode("11511")
            .Build();

        var billing = new Address.Builder("5 Pyramids", "Giza", "Egypt")
            .SetZipCode("12556")
            .Build();

        var order = new Order.Builder(DateTime.Today, PaymentMethod.Visa, "EGP", 1000m)
            .SetDiscount(100m)
            .SetTax(126m)
            .Build();

        var newInvoice = new NewInvoice.Builder(1, "Ahmed Ali", "01000000000", shipping, order)
            .SetCustomerEmail("ahmed@example.com")
            .SetBillingAddress(billing)
            .Build();

        Console.ReadLine();
    }
}
