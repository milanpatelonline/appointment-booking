using AppointmentBooking.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AppointmentBooking.DAL.DataContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<SPGetUserProfile> SPGetUserProfile { get; set; }
        public DbSet<SPGetUserProfile> SPGetAllUser { get; set; }        
        public DbSet<SPGetStaffUser> SPGetStaffUser { get; set; }
        public DbSet<AppointmentDetails> SPGetAppointmentById { get; set; }
        public DbSet<AppointmentDetails> SPGetTodayAppointment { get; set; }

        //public DbSet<RoleMaster> RoleMaster { get; set; }
        //public DbSet<UserRoleMaster> UserRoleMaster { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Appointment>()
            //    .HasOne(a => a.CreatedBy)
            //    .WithMany()
            //    .HasForeignKey(a => a.CreatedById)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Appointment>()
            //    .HasOne(a => a.AssignedTo)
            //    .WithMany()
            //    .HasForeignKey(a => a.AssignedToId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SPGetUserProfile>().HasNoKey();
        }
    }
}
