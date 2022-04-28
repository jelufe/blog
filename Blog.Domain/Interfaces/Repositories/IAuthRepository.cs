﻿using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<User> GetUserByCredentials(AuthDto auth);
        Task<User> GetUserByGoogleId(string googleId);
        Task<User> GetUserByEmail(string email);
        Task<bool> ChangePassword(User user, string newPassword);
        Task<User> ChangePassword(PasswordDto auth);
    }
}
