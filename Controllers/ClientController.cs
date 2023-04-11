using HotelMgmtSys.Data;
using HotelMgmtSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelMgmtSys.Controllers
{
    public class ClientController : Controller
    {
        private readonly HotelManagementDbContext _dbContext;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            // Get the current client's details
            var clientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var client = _dbContext.Clients.Include(c => c.Bookings).FirstOrDefault(c => c.Id == Convert.ToInt32(clientId));

            // Get the client's current bookings
            var currentDate = DateTime.Today;
            var currentBookings = client.Bookings.Where(b => b.CheckInDate <= currentDate && b.CheckOutDate >= currentDate).ToList();

            // Get the client's past bookings
            var pastBookings = client.Bookings.Where(b => b.CheckOutDate < currentDate).ToList();

            // Initialize the view model
            var viewModel = new ClientDashboardViewModel
            {
                Client = client,
                CurrentBookings = currentBookings,
                PastBookings = pastBookings
            };

            return View(viewModel);
        }
    }
}
