using HotelMgmtSys.Data;
using HotelMgmtSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace HotelMgmtSys.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HotelManagementDbContext _context;

        /*public HomeController(HotelManagementDbContext context)
        {
            _context = context;
           // _logger = logger;

        }*/
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(RoomSearch model)
        {
            var rooms = _context.Rooms.ToList();

            // create a list of view models for the search results
            var searchResults = rooms.Select(r => new RoomSearch
            {
                RoomType = r.RoomType,
                Fare = r.Price,
                Capacity = r.Capacity,
                RoomId = r.Id
            }).ToList();

            // pass the search results to the view
            return View("SearchResults", searchResults);
        }

        [HttpPost]
        public ActionResult BookRoom(RoomBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If model state is invalid, return to the search results page with the same search criteria.
                var searchCriteria = new RoomSearch
                {
                    FromDate = model.CheckInDate,
                    ToDate = model.CheckOutDate,
                    FromTime = model.FromTime,
                    ToTime = model.ToTime,
                    RoomType = model.RoomType
                };
                ViewBag.SearchCriteria = searchCriteria;
                var rooms = _context.Rooms.ToList();
                return View("SearchResults", rooms);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If user is authenticated, add the booking to their account and show a confirmation page.
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = new RoomBooking
                {
                    UserId = int.Parse(userId),
                    RoomId = model.RoomId,
                    CheckInDate = model.CheckInDate,
                    CheckOutDate = model.CheckOutDate,
                    FromTime = model.FromTime,
                    ToTime = model.ToTime,
                    BookingDate = DateTime.Now
                };
                _context.RoomBookings.Add(booking);
                _context.SaveChanges();
                return View("BookingConfirmation");
            }
            else
            {
                // If user is not authenticated, show the appropriate sign-up or login page.
                if (model.GuestCheckout)
                {
                    // If guest checkout was selected, show the guest checkout page.
                    return View("GuestCheckout", model);
                }
                else
                {
                    // If not a guest checkout, redirect to the appropriate page with a return URL to this action.
                    var returnUrl = Url.Action("BookRoom", new
                    {
                        roomId = model.RoomId,
                        checkInDate = model.CheckInDate,
                        checkOutDate = model.CheckOutDate,
                        fromTime = model.FromTime,
                        toTime = model.ToTime,
                        roomType = model.RoomType
                    });
                    if (model.IsSignUp)
                    {
                        // If sign-up was selected, redirect to the sign-up page with the return URL.
                        return RedirectToAction("SignUp", "Account", new { ReturnUrl = returnUrl });
                    }
                    else
                    {
                        // If login was selected, redirect to the login page with the return URL.
                        return RedirectToAction("Login", "Account", new { ReturnUrl = returnUrl });
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult GuestCheckoutConfirmation(RoomBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the booking details to the database
                var roomBooking = new RoomBooking
                {
                    RoomId = model.RoomId,
                    UserId = null,
                    CheckInDate = model.CheckInDate,
                    CheckOutDate = model.CheckOutDate,
                    FromTime = model.FromTime,
                    ToTime = model.ToTime,
                    TotalFare = model.TotalFare,
                    IsConfirmed = true
                };

                _context.RoomBookings.Add(roomBooking);
                _context.SaveChanges();

                // Redirect to confirmation page
                return RedirectToAction("GuestCheckoutConfirmationNext", new { bookingId = roomBooking.Id });
            }

            // If the model is not valid, return the checkout view with errors
            return View("GuestCheckout", model);
        }

        public ActionResult GuestCheckoutConfirmationNext(int bookingId)
        {
            // Get the booking details from the database
            var booking = _context.RoomBookings
                .Include(rb => rb.Room)
                .FirstOrDefault(rb => rb.Id == bookingId);

            if (booking == null)
            {
                // If the booking is not found, redirect to the homepage
                return RedirectToAction("Index");
            }

            // Return the confirmation view with the booking details
            return View("GuestConfirmation", booking);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
