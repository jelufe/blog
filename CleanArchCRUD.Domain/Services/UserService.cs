using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchCRUD.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUser(int id)
        {
            var userFound = await _userRepository.GetUser(id);

            if (userFound == null)
                throw new Exception("User does not exist");

            return userFound;
        }

        public async Task InsertUser(User user)
        {
            await _userRepository.InsertUser(user);
        }

        public async Task<bool> UpdateUser(User user)
        {
            var userFound = await _userRepository.GetUser(user.UserId);

            if (userFound == null)
                throw new Exception("User does not exist");

            return await _userRepository.UpdateUser(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userFound = await _userRepository.GetUser(id);

            if (userFound == null)
                throw new Exception("User does not exist");

            return await _userRepository.DeleteUser(id);
        }
    }
}
