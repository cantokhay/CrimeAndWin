using CrimeAndWin.Contracts.Events.Action;
using CrimeAndWin.Saga.StateMachines;
using CrimeAndWin.Saga.States;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CrimeAndWin.Saga.Test.StateMachines;

public class CrimeRewardStateMachineTests
{
    [Fact]
    public async Task CrimeCompleted_WithItem_TransitionsToWaitingForInventory()
    {
        // Arrange
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddSagaStateMachine<CrimeRewardStateMachine, CrimeRewardState>();
            })
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        var correlationId = Guid.NewGuid();
        var itemId = Guid.NewGuid();

        // Act
        await harness.Bus.Publish(new CrimeCompletedEvent
        {
            CorrelationId = correlationId,
            ItemRewardId = itemId,
            IsSuccess = true,
            MoneyReward = 100
        });

        var sagaHarness = harness.GetSagaStateMachineHarness<CrimeRewardStateMachine, CrimeRewardState>();
        await sagaHarness.Created.Any(x => x.CorrelationId == correlationId);

        // Economy finishes successfully
        await harness.Bus.Publish(new CrimeAndWin.Contracts.Events.Economy.EconomyRewardedEvent
        {
            CorrelationId = correlationId,
            IsSuccess = true
        });

        // Assert
        var instance = sagaHarness.Created.Contains(correlationId);
        instance.Should().NotBeNull();
        instance!.CurrentState.Should().Be("WaitingForInventory");

        // Check if GrantItemCommand was published
        (await harness.Published.Any<CrimeAndWin.Contracts.Commands.Inventory.GrantItemCommand>()).Should().BeTrue();
    }

    [Fact]
    public async Task EconomyFailed_TransitionsToFailedState()
    {
        // Arrange
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddSagaStateMachine<CrimeRewardStateMachine, CrimeRewardState>();
            })
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        var correlationId = Guid.NewGuid();

        // Act
        await harness.Bus.Publish(new CrimeCompletedEvent { CorrelationId = correlationId, IsSuccess = true });
        
        var sagaHarness = harness.GetSagaStateMachineHarness<CrimeRewardStateMachine, CrimeRewardState>();
        await sagaHarness.Created.Any(x => x.CorrelationId == correlationId);

        await harness.Bus.Publish(new CrimeAndWin.Contracts.Events.Economy.EconomyRewardedEvent
        {
            CorrelationId = correlationId,
            IsSuccess = false,
            FailReason = "Account Frozen"
        });

        // Assert
        var instance = sagaHarness.Created.Contains(correlationId);
        instance.Should().NotBeNull();
        instance!.CurrentState.Should().Be("Failed");
        instance.FailReason.Should().Be("Account Frozen");
    }
}
