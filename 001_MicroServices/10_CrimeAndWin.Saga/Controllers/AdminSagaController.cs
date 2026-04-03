using CrimeAndWin.Saga.Data;
using CrimeAndWin.Saga.States;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrimeAndWin.Saga.Controllers;

[Route("api/saga/admin")]
[ApiController]
public class AdminSagaController(SagaDbContext context) : ControllerBase
{
    [HttpGet("states")]
    public async Task<IActionResult> GetAllSagaStates([FromQuery] string? state = null)
    {
        var rewardStates = await context.CrimeRewardStates.ToListAsync();
        var actionStates = await context.CrimeActionStates.ToListAsync();
        var purchaseStates = await context.PurchaseStates.ToListAsync();
        var rankStates = await context.RankUpdateStates.ToListAsync();

        var all = rewardStates.Select(s => new SagaStateSummary(s.CorrelationId, "CrimeReward", s.PlayerId, s.CurrentState, s.CreatedAt, s.CompletedAt, s.FailReason))
            .Concat(actionStates.Select(s => new SagaStateSummary(s.CorrelationId, "CrimeAction", s.PlayerId, s.CurrentState, s.CreatedAt, null, s.FailReason)))
            .Concat(purchaseStates.Select(s => new SagaStateSummary(s.CorrelationId, "Purchase", s.PlayerId, s.CurrentState, s.CreatedAt, null, s.FailReason)))
            .Concat(rankStates.Select(s => new SagaStateSummary(s.CorrelationId, "RankUpdate", s.PlayerId, s.CurrentState, s.CreatedAt, null, s.FailReason)));

        if (!string.IsNullOrEmpty(state))
        {
            all = all.Where(x => x.CurrentState.Equals(state, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(all.OrderByDescending(x => x.CreatedAt));
    }

    [HttpGet("states/{id:guid}")]
    public async Task<IActionResult> GetSagaDetail(Guid id)
    {
        var rewardState = await context.CrimeRewardStates.FirstOrDefaultAsync(x => x.CorrelationId == id);
        if (rewardState != null)
        {
            return Ok(new SagaDetailResponse
            {
                CorrelationId = rewardState.CorrelationId,
                SagaType = "CrimeReward",
                PlayerId = rewardState.PlayerId,
                CurrentState = rewardState.CurrentState,
                CreatedAt = rewardState.CreatedAt,
                CompletedAt = rewardState.CompletedAt,
                FailReason = rewardState.FailReason,
                EconomyDone = rewardState.EconomyDone,
                InventoryDone = rewardState.InventoryDone,
                ProfileDone = rewardState.ProfileDone
            });
        }

        var actionState = await context.CrimeActionStates.FirstOrDefaultAsync(x => x.CorrelationId == id);
        if (actionState != null)
        {
            return Ok(new SagaDetailResponse { CorrelationId = actionState.CorrelationId, SagaType = "CrimeAction", PlayerId = actionState.PlayerId, CurrentState = actionState.CurrentState, CreatedAt = actionState.CreatedAt, FailReason = actionState.FailReason });
        }

        var purchaseState = await context.PurchaseStates.FirstOrDefaultAsync(x => x.CorrelationId == id);
        if (purchaseState != null)
        {
            return Ok(new SagaDetailResponse { CorrelationId = purchaseState.CorrelationId, SagaType = "Purchase", PlayerId = purchaseState.PlayerId, CurrentState = purchaseState.CurrentState, CreatedAt = purchaseState.CreatedAt, FailReason = purchaseState.FailReason });
        }

        var rankState = await context.RankUpdateStates.FirstOrDefaultAsync(x => x.CorrelationId == id);
        if (rankState != null)
        {
            return Ok(new SagaDetailResponse { CorrelationId = rankState.CorrelationId, SagaType = "RankUpdate", PlayerId = rankState.PlayerId, CurrentState = rankState.CurrentState, CreatedAt = rankState.CreatedAt, FailReason = rankState.FailReason });
        }

        return NotFound();
    }

    public record SagaStateSummary(Guid CorrelationId, string SagaType, Guid PlayerId, string CurrentState, DateTime CreatedAt, DateTime? CompletedAt, string? FailReason);

    public class SagaDetailResponse
    {
        public Guid CorrelationId { get; set; }
        public string SagaType { get; set; } = string.Empty;
        public Guid PlayerId { get; set; }
        public string CurrentState { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? FailReason { get; set; }
        public bool EconomyDone { get; set; }
        public bool InventoryDone { get; set; }
        public bool ProfileDone { get; set; }
    }
}


