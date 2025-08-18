using AppointmentBooking.Business.Contract;
using AppointmentBooking.DAL.DataContext;
using AppointmentBooking.Models.Models;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(AppDbContext context, UserManager<ApplicationUser> userManager,
            IAppointmentService appointmentService)
        {
            _context = context;
            _userManager = userManager;
            _appointmentService = appointmentService;
        }

        // GET: /api/appointments
        [HttpGet]
        public async Task<IActionResult> GetTodayAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            var response = await _appointmentService.GetTodayAppointment();
            if (response != null && response.IsSuccess && response.Result.Count>0) {
                
                if (roles.Contains("Applicant"))
                    response.Result = (List<AppointmentDetails>)response.Result.Where(a => a.CreatedById == userId.ToString()).ToList();
                else if (roles.Contains("Staff"))
                    response.Result = (List<AppointmentDetails>)response.Result.Where(a => a.AssignedToId == userId.ToString()).ToList();
            }

            return Ok(response);
        }

        // GET: /api/appointments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            return Ok(await _appointmentService.GetAppointmentById(id));
        }

        // POST: /api/appointments
        [HttpPost]
        [Authorize(Roles = "Applicant")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppointment(Appointment model)
        {
            var response = new ApiGenericResponseModel<Appointment>();
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (model.AppointmentDateTime < DateTime.UtcNow)
                {
                    throw new ("Cannot book an appointment in the past.");
                }

                model.CreatedById = userId;
                model.AppointmentDateTime = DateTime.SpecifyKind(model.AppointmentDateTime, DateTimeKind.Local);
                response = await _appointmentService.CreateAppointment(model);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
                throw;
            }

            return Ok(response);
        }

        // PUT: /api/appointments/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment updated)
        {
            updated.Id = id;
            await _appointmentService.UpdateAppointment(updated);
            return Ok(updated);
        }

        // DELETE: /api/appointments/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result= await _appointmentService.DeleteAppointment(id);
            return Ok(result);
        }


        // GET: /api/appointments/staff-users
        [HttpGet("staff-users")]
        public async Task<IActionResult> GetStaffUsers()
        {
            return Ok(await _appointmentService.GetStaffUser());

        }

        // GET: /api/appointments/staff-users
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _appointmentService.GetAllUsers());

        }
    }
}
