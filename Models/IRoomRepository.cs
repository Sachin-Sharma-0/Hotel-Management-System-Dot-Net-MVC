using HotelMgmtSys.Data;

namespace HotelMgmtSys.Models
{
    public interface IRoomRepository
    {
        HotelManagementDbContext _context { get; }
        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms;
        }
        Room GetRoomById(int id);
        void UpdateRoom(Room room);
        void CreateRoom(Room room);
        void DeleteRoom(int id);

        void AssignRoom(int roomId, string clientId, DateTime startDate, DateTime endDate, int numOfGuests)
        {
            var room = _context.Rooms.Find(roomId);

            if (room == null)
            {
                throw new ArgumentException("Invalid Room Id");
            }

            if (room.Status != Room.RoomStatus.Available)
            {
                throw new ArgumentException("Selected room is not available.");
            }

            var client = _context.Clients.Find(clientId);

            if (client == null)
            {
                throw new ArgumentException("Invalid Client Id");
            }

            var booking = new Booking
            {
                Room = room,
                Client = client,
                FromDate = startDate,
                ToDate = endDate,
                Status = BookingStatus.BookingStatusEnum.Confirmed.ToString()
            };

            _context.Bookings.Add(booking);
            room.Status = Room.RoomStatus.Occupied;
            _context.SaveChanges();
        }
    }
}
