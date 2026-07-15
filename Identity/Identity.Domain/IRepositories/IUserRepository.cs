using Identity.Identity.Domain.Models;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModels;

namespace Module.Identity.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsername(
  string username,
  Func<IQueryable<User>, IQueryable<User>>? include = null,
  bool isTracked = true);

        Task<User?> GetByEmail(
  string email,
  Func<IQueryable<User>, IQueryable<User>>? include = null,
  bool isTracked = true);


        Task<User?> GetById(
  int Id,
  Func<IQueryable<User>, IQueryable<User>>? include = null,
  bool isTracked = true);




        public Task<List<UserLookupDto>> GetUsersByIdsAsync(
            List<int> userIds);


        Task<bool> CheckUserExistsAsync(
             AddNewUserDTO request);



        Task<User> Add(User entity);


        Task<PagedResult<UserInfoDTO>> GetUsersAsync(
           GetUsersRequest request,
           Func<IQueryable<User>, IQueryable<User>>? include = null,
           bool isTracked = true);
    }






}
