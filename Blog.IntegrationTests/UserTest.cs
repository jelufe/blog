using Blog.Infrastructure.Data;
using Blog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using Blog.Domain.Entities;
using Blog.Domain.Services;

namespace Blog.IntegrationTests
{
    public class UserTest
    {
        [Fact]
        public async Task GetUsersTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BlogContext(options);

            UserRepository userRepository = new UserRepository(context);
            UserService userService = new UserService(userRepository);

            var result = await userService.GetUsers();
            Assert.Empty(result);

            var user = new User
            {
                Email = "teste@teste.com",
                Type = "Administrator",
                Name = "Teste",
                Password = "teste"
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            var result2 = await userService.GetUsers();
            Assert.Single(result2);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetUserTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BlogContext(options);

            UserRepository userRepository = new UserRepository(context);

            UserService userService = new UserService(userRepository);

            var user = new User
            {
                Email = "teste@teste.com",
                Name = "Teste",
                Type = "Administrator",
                Password = "teste"
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            var result = await userService.GetUser(user.UserId);
            Assert.Equal(result.UserId, user.UserId);
            Assert.Equal(result.Name, user.Name);
            Assert.Equal(result.Type, user.Type);
            Assert.Equal(result.Email, user.Email);
            Assert.Equal(result.Password, user.Password);

            var ex = await Assert.ThrowsAsync<Exception>(() => userService.GetUser(0));
            Assert.Equal("User does not exist", ex.Message);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task InsertUser()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BlogContext(options);

            UserRepository userRepository = new UserRepository(context);

            UserService userService = new UserService(userRepository);

            var rows = await context.Users.CountAsync();

            Assert.Equal(0, rows);

            var user = new User
            {
                Email = "teste@teste.com",
                Name = "Teste",
                Type = "Administrator",
                Password = "teste"
            };

            await userService.InsertUser(user);

            var rows2 = await context.Users.CountAsync();

            Assert.Equal(1, rows2);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdateUser()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BlogContext(options);

            UserRepository userRepository = new UserRepository(context);

            UserService userService = new UserService(userRepository);

            var user = new User
            {
                Email = "teste@teste.com",
                Name = "Teste",
                Type = "Administrator",
                Password = "teste"
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            user.Name = "TesteAtualizado";

            var result = await userService.UpdateUser(user);

            Assert.True(result);

            user.UserId = 0;

            var ex = await Assert.ThrowsAsync<Exception>(() => userService.UpdateUser(user));
            Assert.Equal("User does not exist", ex.Message);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task DeleteUser()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BlogContext(options);

            UserRepository userRepository = new UserRepository(context);

            UserService userService = new UserService(userRepository);

            var user = new User
            {
                Email = "teste@teste.com",
                Name = "Teste",
                Type = "Administrator",
                Password = "teste"
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            var result = await userService.DeleteUser(user.UserId);

            Assert.True(result);

            var ex = await Assert.ThrowsAsync<Exception>(() => userService.DeleteUser(user.UserId));
            Assert.Equal("User does not exist", ex.Message);

            context.Database.EnsureDeleted();
        }
    }
}
