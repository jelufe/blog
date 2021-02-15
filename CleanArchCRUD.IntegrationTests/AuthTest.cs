using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Services;
using CleanArchCRUD.Infrastructure.Data;
using CleanArchCRUD.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchCRUD.IntegrationTests
{
    public class AuthTest
    {
        [Fact]
        public async Task GetUserByCredentials()
        {
            var options = new DbContextOptionsBuilder<CleanArchCRUDContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CleanArchCRUDContext(options);

            AuthRepository authRepository = new AuthRepository(context);
            AuthService authService = new AuthService(authRepository);

            var user = new User
            {
                Email = "teste@teste.com",
                Name = "Teste",
                Password = "teste"
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            var auth = new AuthDto
            {
                Email = "teste@teste.com",
                Password = "teste"
            };

            var result = await authService.GetUserByCredentials(
                auth,
                "Tfqweqw1234232sfwqfcdfsdg342352265gsdg",
                "https://localhost:44367/",
                "https://localhost:44367/"
            );

            Assert.True(!String.IsNullOrEmpty(result));

            var auth2 = new AuthDto
            {
                Email = "teste2@teste.com",
                Password = "teste2"
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => authService.GetUserByCredentials(
                auth2,
                "Tfqweqw1234232sfwqfcdfsdg342352265gsdg",
                "https://localhost:44367/",
                "https://localhost:44367/"
            ));

            Assert.Equal("Incorrect email or password", ex.Message);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ChangePassword()
        {
            var options = new DbContextOptionsBuilder<CleanArchCRUDContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CleanArchCRUDContext(options);

            AuthRepository authRepository = new AuthRepository(context);
            AuthService authService = new AuthService(authRepository);

            var user = new User
            {
                Email = "teste@teste.com",
                Name = "Teste",
                Password = "teste"
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            var password = new PasswordDto
            {
                Email = "teste@teste.com",
                OldPassword = "teste",
                NewPassword = "teste2"
            };

            var result = await authService.ChangePassword(password);

            Assert.True(result);

            var ex = await Assert.ThrowsAsync<Exception>(() => authService.ChangePassword(password));

            Assert.Equal("Incorrect email or oldPassword", ex.Message);

            context.Database.EnsureDeleted();
        }
    }
}
