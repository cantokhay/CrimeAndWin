using System.Linq.Expressions;
using FluentAssertions;
using Identity.Application.DTOs.AuthDTOs;
using Identity.Application.Features.Auth;
using Identity.Application.Features.Auth.Abstract;
using Identity.Application.Features.Auth.Commands.Login;
using Identity.Domain.Entities;
using Moq;
using Shared.Domain.Repository;
using Xunit;

namespace Identity.Application.Test.Features.Auth.Commands.Login
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IReadRepository<AppUser>> _mockUsersRead;
        private readonly Mock<IReadRepository<UserRole>> _mockUserRolesRead;
        private readonly Mock<IReadRepository<Role>> _mockRolesRead;
        private readonly Mock<IReadRepository<UserClaim>> _mockUserClaimsRead;
        private readonly Mock<IReadRepository<RoleClaim>> _mockRoleClaimsRead;
        private readonly Mock<IWriteRepository<RefreshToken>> _mockRefreshWrite;
        private readonly Mock<IJwtTokenGenerator> _mockJwt;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _mockUsersRead = new Mock<IReadRepository<AppUser>>();
            _mockUserRolesRead = new Mock<IReadRepository<UserRole>>();
            _mockRolesRead = new Mock<IReadRepository<Role>>();
            _mockUserClaimsRead = new Mock<IReadRepository<UserClaim>>();
            _mockRoleClaimsRead = new Mock<IReadRepository<RoleClaim>>();
            _mockRefreshWrite = new Mock<IWriteRepository<RefreshToken>>();
            _mockJwt = new Mock<IJwtTokenGenerator>();

            // Note: LoginCommandHandler is expected to be refactored to inject IPasswordHasher for full testability.
            // Currently using a real instance if possible or mocking the result if injected.
            
            _handler = new LoginCommandHandler(
                _mockUsersRead.Object,
                _mockUserRolesRead.Object,
                _mockRolesRead.Object,
                _mockUserClaimsRead.Object,
                _mockRoleClaimsRead.Object,
                _mockRefreshWrite.Object,
                _mockJwt.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsUnauthorizedException()
        {
            // Arrange
            var command = new LoginCommand 
            { 
                UserNameOrEmail = "nonexistent", 
                Password = "Password123" 
            };

            _mockUsersRead.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<AppUser, bool>>>(), false))
                .ReturnsAsync((AppUser)null!);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Kullanıcı adı/e-posta veya şifre hatalı.");
        }

        [Fact]
        public async Task Handle_IncorrectPassword_ThrowsUnauthorizedException()
        {
            // Arrange
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<AppUser>();
            var user = new AppUser 
            { 
                Id = Guid.NewGuid(), 
                UserName = "testuser", 
                PasswordHash = hasher.HashPassword(null!, "CorrectPassword123") 
            };

            _mockUsersRead.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<AppUser, bool>>>(), false))
                .ReturnsAsync(user);

            var command = new LoginCommand 
            { 
                UserNameOrEmail = "testuser", 
                Password = "WrongPassword123" 
            };

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Kullanıcı adı/e-posta veya şifre hatalı.");
        }

        [Fact]
        public async Task Handle_ValidCredentials_ReturnsAuthResult()
        {
            // Arrange
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<AppUser>();
            var user = new AppUser 
            { 
                Id = Guid.NewGuid(), 
                UserName = "testuser", 
                Email = "test@example.com",
                PasswordHash = hasher.HashPassword(null!, "Password123") 
            };

            var command = new LoginCommand 
            { 
                UserNameOrEmail = "testuser", 
                Password = "Password123" 
            };

            _mockUsersRead.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<AppUser, bool>>>(), false))
                .ReturnsAsync(user);

            _mockJwt.Setup(x => x.Generate(user, It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<KeyValuePair<string, string>>>()))
                .Returns(("access_token", DateTime.UtcNow.AddHours(1)));
            
            _mockJwt.Setup(x => x.GenerateRefreshToken())
                .Returns("refresh_token");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.AccessToken.Should().Be("access_token");
            result.RefreshToken.Should().Be("refresh_token");
        }
    }
}

