using AppointmentBooking.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBooking.DAL.Contract
{
    public interface IAppointmentRepository
    {
        Task<ApiGenericResponseModel<Appointment>> CreateAppointment(Appointment data, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<bool>> DeleteAppointment(int id, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<AppointmentDetails>> GetAppointmentById(int id, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<bool>> UpdateAppointment(Appointment data, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<List<SPGetStaffUser>>> GetStaffUser(CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<List<SPGetUserProfile>>> GetAllUsers(CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<List<AppointmentDetails>>> GetTodayAppointment(CancellationToken cancellationToken = default);
    }
}
