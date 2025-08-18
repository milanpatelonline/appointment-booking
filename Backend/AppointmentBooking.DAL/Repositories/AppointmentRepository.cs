using AppointmentBooking.DAL.Contract;
using AppointmentBooking.DAL.DataContext;
using AppointmentBooking.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace AppointmentBooking.DAL.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _dbContext;

        public AppointmentRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ApiGenericResponseModel<Appointment>> CreateAppointment(Appointment data, CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<Appointment>();
            response.ErrorMessage = new List<string>();

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                try
                {
                    if (!await _dbContext.Users.AnyAsync(u => u.Id == data.AssignedToId))
                        throw new Exception("Invalid staff ID");

                    var appointmentStart = data.AppointmentDateTime;
                    var appointmentEnd = appointmentStart.AddHours(1);

                    bool isStaffAvailable = !await _dbContext.Appointments.AnyAsync(a => a.AssignedToId == data.AssignedToId &&
                    a.AppointmentDateTime < appointmentEnd &&
                    a.AppointmentDateTime.AddHours(1) > appointmentStart);

                    if (!isStaffAvailable)
                        throw new Exception("The staff member is not available at this time.");
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage.Add(ex.Message);
                    return response;
                }

                await _dbContext.Appointments.AddAsync(data, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);                
                response.IsSuccess = true;
                response.Result = data;
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync(cancellationToken);

                if (e.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    Console.WriteLine(e);
                    response.ErrorMessage.Add(e.InnerException.Message);
                }

                Console.WriteLine(e);
                response.IsSuccess = false;
                response.ErrorMessage.Add(e.InnerException?.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<bool>> DeleteAppointment(int id, CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<bool>();
            response.ErrorMessage = new List<string>();

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                response.IsSuccess = true;
                var record = await _dbContext.Appointments.Where(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);

                if (record == null)
                    return response;

                _dbContext.Appointments.Remove(record);

                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync(cancellationToken);
                Console.WriteLine(e);
                response.ErrorMessage.Add(e.Message);
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<AppointmentDetails>> GetAppointmentById(int id, CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<AppointmentDetails>();
            response.ErrorMessage = new List<string>();

            try
            {
               StringBuilder spString = new StringBuilder("SP_GetAppointmentById");
                spString.Append(" @Id =" + "'" + id.ToString() + "'");

                var AppointmentResult = await _dbContext.SPGetAppointmentById
                   .FromSqlRaw<AppointmentDetails>(spString.ToString()).ToListAsync();               

                response.IsSuccess = true;
                response.Result = AppointmentResult.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<List<SPGetStaffUser>>> GetStaffUser(CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<List<SPGetStaffUser>>();
            response.ErrorMessage = new List<string>();
            
            try
            {
                string spString = "SP_GetStaffUser";
                var staffUserResult = await _dbContext.SPGetStaffUser
                   .FromSqlRaw<SPGetStaffUser>(spString).ToListAsync();
                
                response.IsSuccess = true;
                response.Result = staffUserResult;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<List<SPGetUserProfile>>> GetAllUsers(CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<List<SPGetUserProfile>>();
            response.ErrorMessage = new List<string>();

            try
            {
                string spString = "SP_GetAllUser";
                var users = await _dbContext.SPGetAllUser
                   .FromSqlRaw<SPGetUserProfile>(spString).ToListAsync();

                response.IsSuccess = true;
                response.Result = users;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<bool>> UpdateAppointment(Appointment data, CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<bool>();
            response.IsSuccess = true;

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var appointment = await _dbContext.Appointments.FindAsync(data.Id);
                if (appointment == null)
                    return response;

                appointment.AppointmentDateTime = data.AppointmentDateTime;
                appointment.AssignedToId = data.AssignedToId;
                appointment.Details = data.Details;

                await _dbContext.SaveChangesAsync();
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync(cancellationToken);

                if (e.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    Console.WriteLine(e);
                    response.ErrorMessage.Add($"Constraint violation: {e.InnerException.Message}");
                }

                Console.WriteLine(e);
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<List<AppointmentDetails>>> GetTodayAppointment(CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<List<AppointmentDetails>>();
            response.ErrorMessage = new List<string>();

            try
            {
                string spString = "SP_GetTodayAppointment";               

                var AppointmentResult = await _dbContext.SPGetTodayAppointment
                   .FromSqlRaw<AppointmentDetails>(spString.ToString()).ToListAsync();

                response.IsSuccess = true;
                response.Result = AppointmentResult;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }
    }
}
