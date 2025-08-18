using AppointmentBooking.Models.Models;

namespace AppointmentBooking.DAL.Contract
{
    public interface IUserMasterRepository
    {
        Task<ApiGenericResponseModel<bool>> Create(UserMaster data, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<TokenModel>> GetUserProfile(Guid id, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<List<SPGetUserProfile>>> GetAllUser(CancellationToken cancellationToken = default);
    }
}
