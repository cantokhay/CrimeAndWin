using System;
using System.Threading.Tasks;

namespace Action.Application.Abstract;

public interface IPlayerProfileService
{
    Task<int> GetPlayerLevelAsync(Guid playerId);
}


