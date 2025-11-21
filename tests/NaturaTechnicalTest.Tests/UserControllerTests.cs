using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Necessário para IStatusCodeHttpResult
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading.Tasks;
using Natura.TechnicalTest.Core.Entities;
using Natura.TechnicalTest.Core.Services;
using Natura.TechnicalTest.Infrastructure.Data;
using Natura.TechnicalTest.Features.Users;

namespace NaturaTechnicalTest.Tests
{
    public class UsersControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Elevate_ShouldReturnBadRequest_WhenAdminTriesToElevateThemselves()
        {
            //  ARRANGE
            var dbContext = GetInMemoryDbContext();
            
            var adminUser = new User { Id = 99, Name = "Admin", Email = "admin@test.com", Role = "admin" };
            dbContext.Users.Add(adminUser);
            await dbContext.SaveChangesAsync();

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(s => s.GetCurrentUserAsync()).ReturnsAsync(adminUser);

            var result = await UsersEndpoints.Elevate(99, dbContext, mockUserService.Object);


            var response = Assert.IsAssignableFrom<IStatusCodeHttpResult>(result);
            

            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task Elevate_ShouldSuccess_WhenAdminElevatesAnotherUser()
        {
            // ARRANGE
            var dbContext = GetInMemoryDbContext();

            var adminUser = new User { Id = 1, Name = "Admin", Role = "admin", Email = "a@a.com" };
            var targetUser = new User { Id = 2, Name = "Bob", Role = "user", Email = "b@b.com" };
            
            dbContext.Users.AddRange(adminUser, targetUser);
            await dbContext.SaveChangesAsync();

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(s => s.GetCurrentUserAsync()).ReturnsAsync(adminUser);

            // ACT
            var result = await UsersEndpoints.Elevate(2, dbContext, mockUserService.Object);

            // ASSERT
            var okResult = Assert.IsType<Ok<User>>(result);
            Assert.Equal(200, okResult.StatusCode);

            var userInDb = await dbContext.Users.FindAsync(2);
            Assert.Equal("admin", userInDb.Role);
        }
    }
}