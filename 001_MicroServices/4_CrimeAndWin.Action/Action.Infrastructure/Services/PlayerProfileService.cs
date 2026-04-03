using System;
using System.Threading.Tasks;
using Action.Application.Abstract;

namespace Action.Infrastructure.Services;

public class PlayerProfileService : IPlayerProfileService
{
    // In a real environment, this would call the PlayerProfile microservice.
    public Task<int> GetPlayerLevelAsync(Guid playerId)
    {
        return Task.FromResult(1); // Default to level 1 for MVP/Demo
    }
}


