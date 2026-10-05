namespace SRC;

class Guest
{
    public int GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }
    private readonly List<Reservation> _reservations = new();
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();


    public Guest(int guestId, string fullName, string phoneNumber)
    {
        if (guestId <= 0)
            throw new ArgumentException("Guest id must be positive.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Guest full name cannot be empty.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Guest phone number cannot be empty.", nameof(phoneNumber));

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public Reservation CreateReservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
    {
        var reservation = new Reservation(reservationId,checkInDate,checkOutDate,room);

        _reservations.Add(reservation);

        return reservation;
    }
}
