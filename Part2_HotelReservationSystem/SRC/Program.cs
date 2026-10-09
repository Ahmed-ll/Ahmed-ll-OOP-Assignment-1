namespace SRC;

class Program
{
    static void Main(string[] args)
    {
        // Room room = new Room(-701, RoomType.Single, 100.00m); // will throw an exception because room number is negative
        //Guest guest = new Guest(-1, "John Doe", "123-456-7890"); // will throw an exception because guest id is negative

        Room room = new Room(5, RoomType.Single, 100.00m);

        Guest guest01 = new Guest(1, "John Doe", "123-456-7890");
        Guest guest02 = new Guest(2, "Jane Smith", "098-765-4321");

        Reservation reservation = guest01.CreateReservation(1, DateTime.Now, DateTime.Now.AddDays(2), room);

        // will throw an exception because the room is already booked for these dates
        // Reservation reservation2 = guest02.CreateReservation(2, DateTime.Now, DateTime.Now.AddDays(2), room); 
    }
}
