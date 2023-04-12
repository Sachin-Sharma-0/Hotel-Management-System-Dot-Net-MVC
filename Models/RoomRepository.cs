using HotelMgmtSys.Data;
using HotelMgmtSys.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMgmtSys.Models
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelManagementDbContext _context;

        /*public RoomRepository(HotelManagementDbContext context)
        {
            _context = context;
        }*/
        public HotelManagementDbContext Context => _context;
        HotelManagementDbContext IRoomRepository._context => throw new NotImplementedException();

        public async void CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async void DeleteRoom(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public async void UpdateRoom(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            return await _context.Rooms
                .Where(r => !r.Bookings.Any(b => (startDate >= b.CheckInDate && startDate <= b.CheckOutDate) ||
                                                 (endDate >= b.CheckInDate && endDate <= b.CheckOutDate)))
                .ToListAsync();
        }

        public async Task<List<Room>> GetRoomsByCategory(Room.RoomCategory category)
        {
            return await _context.Rooms
                .Where(r => r.Category == category)
                .ToListAsync();
        }



        public void AssignRoom(int roomId, DateTime startDate, DateTime endDate)
        {
            var room = _context.Rooms
        .Include(r => r.Bookings)
        .SingleOrDefault(r => r.RoomId == roomId);

            var booking = new RoomBooking
            {
                CheckInDate = startDate,
                CheckOutDate = endDate,
                BookingStatus = BookingStatus.BookingStatusEnum.Confirmed.ToString()
            };

            var roomBooking = new RoomBookingViewModel
            {
                Room = room,
                Booking = booking
            };

            //room.Bookings.Add(booking);
            var newBooking = new Booking
            {
                CheckInDate = roomBooking.Booking.CheckInDate,
                CheckOutDate = roomBooking.Booking.CheckOutDate,
                Status = roomBooking.Booking.BookingStatus
            };

            room.Bookings.Add(newBooking);

            _context.SaveChanges();
        }
    }
}
