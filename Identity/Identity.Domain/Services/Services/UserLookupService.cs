using Identity.Identity.Domain.Models;
using Identity.Identity.Domain.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Module.Identity.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.RequestModels.Identity;

namespace Identity.Identity.Domain.Services.Services
{
    public class UserLookupService (IUserRepository _userRepository) : IUserLookupService
    {
        public async Task<UserLookupDto?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetById(userId);

            var UserData = new UserLookupDto
            {
                Id = user.Id,
                FullName = user.FullName,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted

            };
            return UserData;
        }

        public async Task<List<UserLookupDto>> GetUsersByIdsAsync(List<int> userIds)
        {
            var user = await _userRepository.GetUsersByIdsAsync(userIds);

            return user;
        }
    }
}
