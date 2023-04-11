using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HotelMgmtSys.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
namespace HotelMgmtSys.Data
{
    public class HotelManagementDbContext : DbContext
    {
        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EntryLog> EntryLogs { get; set; }
        public DbSet<ExitLog> ExitLogs { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<GuestCheckout> GuestCheckouts { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
