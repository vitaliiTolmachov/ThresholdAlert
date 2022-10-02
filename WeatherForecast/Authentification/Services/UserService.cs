using Authentification.Models;
using Authentification.Services.Dtos;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DbContext = DataAccess.DbContext;

namespace Authentification.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserNameAsync(string username, string password);

        Task<IEnumerable<UserDto>> GetAllAsync();

    }

    public class UserService : IUserService
    {
        private readonly DbContext _dbContext;

        public UserService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> GetUserNameAsync(string username, string password)
        {
            // on auth fail: null is returned because user is not found
            // on auth success: user object is returned
            var userEntity = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);

            if (userEntity == null)
                return null;

            return new UserDto
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserName = userEntity.Username
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return _dbContext.Users
                .AsNoTracking()
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.Username
                });

        }

    }


}
