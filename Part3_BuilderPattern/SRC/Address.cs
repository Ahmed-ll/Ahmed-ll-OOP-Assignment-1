namespace SRC;

public sealed class Address
{
    public string Street { get; }
    public string City { get; }
    public string Country { get; }
    public string? State { get; }
    public string? ZipCode { get; }


    private Address(
        string street,
        string city,
        string country,
        string? state,
        string? zipCode)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street is required.",nameof(street));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.",nameof(city));

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country is required.",nameof(country));

        Street = street;
        City = city;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public sealed class Builder
    {
        private readonly string _street;
        private readonly string _city;
        private readonly string _country;

        private string? _state;
        private string? _zipCode;

        public Builder(
            string street,
            string city,
            string country)
        {
            _street = street;
            _city = city;
            _country = country;
        }

        public Builder SetState(string state)
        {
            _state = state;
            return this;
        }

        public Builder SetZipCode(string zipCode)
        {
            _zipCode = zipCode;
            return this;
        }

        public Address Build()
        {
            return new Address(
                _street,
                _city,
                _country,
                _state,
                _zipCode);
        }
    }
}