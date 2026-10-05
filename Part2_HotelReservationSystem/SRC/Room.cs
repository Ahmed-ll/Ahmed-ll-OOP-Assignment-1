namespace SRC;

class Room
{
    public int RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; } 
    public bool IsUnderMaintenance { get; private set; }
    private readonly List<Reservation> _reservations = new();

    public Room(int roomNumber, RoomType type, decimal nightlyRate)
    {
        if (roomNumber <= 0)
            throw new ArgumentException("Room number must be positive.");

        if (nightlyRate <= 0)
            throw new ArgumentException("Nightly rate must be greater than zero.",nameof(nightlyRate));
        

        RoomNumber = roomNumber;
        NightlyRate = nightlyRate;
        RoomType = type;
        IsUnderMaintenance = false;
    }

    public void UpdateNightlyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("Nightly rate must be greater than zero.", nameof(newRate));

        NightlyRate = newRate;
    }

    public void StartMaintenance()
    {
        if (IsUnderMaintenance)
            throw new InvalidOperationException($"Room {RoomNumber} is already under maintenance.");

        IsUnderMaintenance = true;
    }
    public void EndMaintenance()
    {
        if (!IsUnderMaintenance)
            throw new InvalidOperationException($"Room {RoomNumber} is not under maintenance.");

        IsUnderMaintenance = false;
    }

    public void EnsureAvailable(DateTime checkIn, DateTime checkOut)
    {
        if (IsUnderMaintenance)
            throw new InvalidOperationException("Cannot book a room that is under maintenance.");

        bool overlaps = _reservations.Any(r => r.IsActive && checkIn < r.CheckOutDate && r.CheckInDate < checkOut);

        if (overlaps)
            throw new InvalidOperationException("Room is already booked for these dates.");
    }

    public void Register(Reservation reservation) => _reservations.Add(reservation);

}