using FluentAssertions;
using GameWorld.Application.Abstract;
using GameWorld.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Xunit;

namespace GameWorld.Test
{
    public class DependencyTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public DependencyTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(typeof(IReadRepository<Domain.Entities.GameWorld>))]
        [InlineData(typeof(IWriteRepository<Domain.Entities.GameWorld>))]
        [InlineData(typeof(IReadRepository<Season>))]
        [InlineData(typeof(IDateTimeProvider))]
        [InlineData(typeof(IEventBus))]
        public void RegisteredServices_ShouldBeResolvable(Type serviceType)
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();

            // Act
            var service = scope.ServiceProvider.GetService(serviceType);

            // Assert
            service.Should().NotBeNull();
        }
    }
}
