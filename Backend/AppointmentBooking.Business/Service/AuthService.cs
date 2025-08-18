using AppointmentBooking.Business.Contract;
using AppointmentBooking.DAL.Contract;
using AppointmentBooking.Models.Models;

namespace AppointmentBooking.Business.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserMasterRepository _repo;
        public AuthService(IUserMasterRepository repo)
        {
            _repo = repo;
        }

        public UserMaster UserProfile { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<ApiGenericResponseModel<bool>> Create(UserMaster data, CancellationToken cancellationToken = default)
        {
            return await _repo.Create(data);
        }

        public async Task<ApiGenericResponseModel<List<SPGetUserProfile>>> GetAllUser(CancellationToken cancellationToken = default)
        {
            return await _repo.GetAllUser(cancellationToken);
        }

        public async Task<ApiGenericResponseModel<TokenModel>> GetUserProfile(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repo.GetUserProfile(id, cancellationToken);
        }

        //public async Task<ApiGenericResponseModel<bool>> Delete(int Id)
        //{
        //    return await _repo.Delete(Id);
        //}
        //public async Task<ApiGenericResponseModel<bool>> Active(int Id)
        //{
        //    return await _repo.Active(Id);
        //}

        //public async Task<ApiGenericResponseModel<User>> GetById(int Id)
        //{
        //    return await _repo.GetById(Id);
        //}

        //public async Task<ApiGenericResponseModel<List<User>>> GetUsers()
        //{
        //    return await _repo.GetUsers();
        //}

        //public async Task<ApiGenericResponseModel<bool>> Update(User data)
        //{
        //    return await _repo.Update(data);
        //}

    }
}
