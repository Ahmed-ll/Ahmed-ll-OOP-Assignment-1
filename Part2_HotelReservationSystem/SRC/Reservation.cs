namespace SRC;

class Reservation
{
    public int ReservationId { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public Room Room { get; }
    public ReservationStatus Status { get; private set; }

    public bool IsActive => Status is not (ReservationStatus.Cancelled or ReservationStatus.CheckedOut);

    public Reservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
    {
        if (room is null)
            throw new ArgumentNullException(nameof(room));

        if (reservationId <= 0)
            throw new ArgumentException("Reservation id must be positive.");

        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Check-out date must be after check-in date.");

        room.EnsureAvailable(checkInDate, checkOutDate);  

        ReservationId = reservationId;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Room = room;
        Status = ReservationStatus.Pending;

        room.Register(this);
    }
    
    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException("Only pending reservations can be confirmed.");
        
        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException( "Only confirmed reservations can be checked in.");

        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        if (Status != ReservationStatus.CheckedIn)
            throw new InvalidOperationException( "Only checked-in reservations can be checked out.");

        Status = ReservationStatus.CheckedOut;
    }


    public void CancelReservation()
    {
        if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException( "Only pending or confirmed reservations can be cancelled.");

        Status = ReservationStatus.Cancelled;
    }

    public decimal GetTotalCost()
    {
        int numberOfNights = (CheckOutDate - CheckInDate).Days;
        return numberOfNights * Room.NightlyRate;
    }
}
