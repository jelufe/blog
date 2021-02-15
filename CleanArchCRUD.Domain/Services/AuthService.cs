﻿using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchCRUD.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<string> GetUserByCredentials(
            AuthDto auth,
            string secretKey,
            string issuer,
            string audience
        )
        {
            var user = await _authRepository.GetUserByCredentials(auth);

            if (user == null)
                throw new Exception("Incorrect email or password");

            return GenerateToken(user, secretKey, issuer, audience);
        }

        public async Task<bool> ChangePassword(PasswordDto password)
        {
            var user = await _authRepository.ChangePassword(password);

            if (user == null)
                throw new Exception("Incorrect email or oldPassword");

            return await _authRepository.ChangePassword(user, password.NewPassword);
        }

        private string GenerateToken(
            User user,
            string secretKey,
            string issuer,
            string audience
        )
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            var payload = new JwtPayload
            (
                issuer,
                audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddHours(6)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
