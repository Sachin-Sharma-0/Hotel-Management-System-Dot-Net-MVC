using HotelMgmtSys.Data;

namespace HotelMgmtSys.Models
{
    public class BookingRepository
    {
        private readonly HotelManagementDbContext _context;


        /*public BookingRepository(HotelManagementDbContext context)
        {
            _context = context;
        }*/
        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }
    }
}
