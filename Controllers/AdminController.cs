using HotelMgmtSys.Data;
using HotelMgmtSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelMgmtSys.Controllers
{
    public class AdminController : Controller
    {
        private readonly HotelManagementDbContext _context;
        private readonly BookingRepository _repository;
        private readonly RoomRepository _roomRepository;


        /*public AdminController(HotelManagementDbContext context, BookingRepository repository, RoomRepository roomRepository)
        {
            _context = context;
            _repository = repository;
            _roomRepository = roomRepository;
        }*/

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            // Count the number of available rooms
            var availableRoomsCount = _context.Rooms.Count(r => r.IsBooked == false);

            // Count the number of occupied rooms
            var occupiedRoomsCount = _context.Rooms.Count(r => r.IsBooked == true);

            // Count the number of registered users
            var registeredUsersCount = _context.Users.Count();

            // Count the number of entry logs for today
            var entryLogsCount = _context.EntryLogs.Count(el => el.DateTime.Date == DateTime.Today);

            // Count the number of exit logs for today
            var exitLogsCount = _context.ExitLogs.Count(ex => ex.DateTime.Date == DateTime.Today);

            // Create a model object with the counts
            var model = new AdminDashboardViewModel
            {
                AvailableRoomsCount = availableRoomsCount,
                OccupiedRoomsCount = occupiedRoomsCount,
                RegisteredUsersCount = registeredUsersCount,
                EntryLogsCount = entryLogsCount,
                ExitLogsCount = exitLogsCount
            };

            // Pass the model object to the view
            return View(model);
        }
        [HttpGet]
        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoom(AddRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room room = new Room
                {
                    RoomType = model.RoomType,
                    Description = model.Description,
                    Price = model.Price
                };
                _repository.AddRoom(room);

                return RedirectToAction("Dashboard");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard");
            }

            return View(room);
        }
        public IActionResult ListClients()
        {
            var clients = _context.Clients.Include(c => c.Room).ToList();
            return View(clients);
        }

        public async Task<IActionResult> AssignRoom()
        {
            var rooms = (await _roomRepository.GetAllRooms().ConfigureAwait(false))
                .AsQueryable()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.RoomNumber} - {r.RoomType}"
                })
                .ToList();

            var viewModel = new AssignRoomViewModel
            {
                Rooms = rooms
            };

            return View(viewModel);
        }

    }
}
