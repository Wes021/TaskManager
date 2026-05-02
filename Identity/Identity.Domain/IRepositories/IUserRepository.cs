using Identity.Identity.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        Task<User?> GetById(
  int Id,
  Func<IQueryable<User>, IQueryable<User>>? include = null,
  bool isTracked = true);



        Task<PagedResult<UserInfoDTO>> GetUsersAsync(
             GetUsersRequest request,
             Func<IQueryable<User>, IQueryable<User>>? include = null,
             bool isTracked = true);


        Task<bool> CheckUserExistsAsync(
             AddNewUserDTO request);



        Task<User> Add(User entity);
    }






}
