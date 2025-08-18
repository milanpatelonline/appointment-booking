using AppointmentBooking.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBooking.Business.Contract
{
    public interface IAuthService
    {
        Task<ApiGenericResponseModel<bool>> Create(UserMaster data, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<TokenModel>> GetUserProfile(Guid id, CancellationToken cancellationToken = default);
        Task<ApiGenericResponseModel<List<SPGetUserProfile>>> GetAllUser(CancellationToken cancellationToken = default);

        //public UserMaster UserProfile { get; set; }
        //Task<ApiGenericResponseModel<User>> GetById(int Id);
        //Task<ApiGenericResponseModel<List<User>>> GetUsers();
        //Task<ApiGenericResponseModel<bool>> Delete(int Id);
        //Task<ApiGenericResponseModel<bool>> Active(int Id);
        //Task<ApiGenericResponseModel<bool>> Update(User data);
    }
}
