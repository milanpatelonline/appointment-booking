using AppointmentBooking.Business.Contract;
using AppointmentBooking.DAL.Contract;
using AppointmentBooking.Models.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentBooking.Business.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<ApiGenericResponseModel<Appointment>> CreateAppointment(Appointment data, CancellationToken cancellationToken = default)
        {
            return await _appointmentRepository.CreateAppointment(data, cancellationToken);
        }

        public async Task<ApiGenericResponseModel<bool>> DeleteAppointment(int id, CancellationToken cancellationToken = default)
        {
            return await _appointmentRepository.DeleteAppointment(id, cancellationToken);
        }

        public async Task<ApiGenericResponseModel<AppointmentDetails>> GetAppointmentById(int id, CancellationToken cancellationToken = default)
        {
            return await _appointmentRepository.GetAppointmentById(id, cancellationToken);
        }

        public async Task<ApiGenericResponseModel<List<SPGetStaffUser>>> GetStaffUser(CancellationToken cancellationToken = default)
        {
            return await _appointmentRepository.GetStaffUser(cancellationToken);
        }

        public async Task<ApiGenericResponseModel<List<SPGetUserProfile>>> GetAllUsers(CancellationToken cancellationToken = default)
        {
            return await _appointmentRepository.GetAllUsers(cancellationToken);
        }

        public async Task<ApiGenericResponseModel<bool>> UpdateAppointment(Appointment data, CancellationToken cancellationToken = default)
        {
            return await _appointmentRepository.UpdateAppointment(data, cancellationToken);
        }

        public async Task<ApiGenericResponseModel<List<AppointmentDetails>>> GetTodayAppointment(CancellationToken cancellationToken = default)
        {
            return await _appointmentRepository.GetTodayAppointment(cancellationToken);
        }
    }
}
