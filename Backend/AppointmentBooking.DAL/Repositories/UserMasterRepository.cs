using AppointmentBooking.DAL.Contract;
using AppointmentBooking.DAL.DataContext;
using AppointmentBooking.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AppointmentBooking.DAL.Repositories
{
    public class UserMasterRepository : IUserMasterRepository
    {
        private readonly AppDbContext _dbContext;

        public UserMasterRepository(AppDbContext context)
        {
            _dbContext = context;
        }     

        public async Task<ApiGenericResponseModel<bool>> Create(UserMaster data, CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<bool>();
            response.ErrorMessage = new List<string>();

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                
                await _dbContext.UserMaster.AddAsync(data, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                response.Result = true;
                response.IsSuccess = true;
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

        public async Task<ApiGenericResponseModel<TokenModel>> GetUserProfile(Guid id, CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<TokenModel>();
            response.ErrorMessage = new List<string>();
            var tokenModel = new TokenModel();
            try
            {
                response.IsSuccess = true;

                StringBuilder spString = new StringBuilder("SP_GetUserProfile");
                spString.Append(" @UserId =" + "'" + id.ToString() + "'" );

                var UserProfileResult = await _dbContext.SPGetUserProfile
                   .FromSqlRaw<SPGetUserProfile>(spString.ToString()).ToListAsync();                

                tokenModel.UserProfile = UserProfileResult.FirstOrDefault();
                response.IsSuccess = true;
                response.Result = tokenModel;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiGenericResponseModel<List<SPGetUserProfile>>> GetAllUser(CancellationToken cancellationToken = default)
        {
            var response = new ApiGenericResponseModel<List<SPGetUserProfile>>();
            response.ErrorMessage = new List<string>();
            var tokenModel = new TokenModel();
            try
            {
                response.IsSuccess = true;

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
    }
}
