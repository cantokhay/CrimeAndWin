using Bogus;
using MediatR;
using Moderation.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Moderation.Application.Features.Report.Commands.Seed
{
    public sealed class RunModerationSeedHandler : IRequestHandler<RunModerationSeedCommand, Unit>
    {
        private readonly IWriteRepository<Domain.Entities.Report> _reportRepo;
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _actionRepo;
        private readonly IDateTimeProvider _clock;

        public RunModerationSeedHandler(
            IWriteRepository<Domain.Entities.Report> reportRepo,
            IWriteRepository<Domain.Entities.ModerationAction> actionRepo,
            IDateTimeProvider clock)
        {
            _reportRepo = reportRepo;
            _actionRepo = actionRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunModerationSeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("tr");

            var reports = new List<Domain.Entities.Report>();
            var actions = new List<Domain.Entities.ModerationAction>();

            for (int i = 0; i < request.Count; i++)
            {
                var reporterId = Guid.NewGuid();
                var reportedId = Guid.NewGuid();

                // 🎯 Rastgele ReportReason seç
                var reason = faker.PickRandom(new[]
                {
                    ReportReason.Hile,
                    ReportReason.Kufur,
                    ReportReason.Taciz,
                    ReportReason.Spam
                });

                var isResolved = faker.Random.Bool(0.4f); // %40 ihtimal çözülmüş

                var report = new Domain.Entities.Report
                {
                    Id = Guid.NewGuid(),
                    ReporterId = reporterId,
                    ReportedPlayerId = reportedId,
                    Reason = reason,
                    Description = faker.Lorem.Sentence(8, 15),
                    IsResolved = isResolved,
                    CreatedAtUtc = _clock.UtcNow.AddHours(-faker.Random.Int(1, 500)),
                    ResolvedAtUtc = isResolved ? _clock.UtcNow.AddHours(-faker.Random.Int(1, 50)) : null,
                    ResolvedByModeratorId = isResolved ? Guid.NewGuid() : null,
                    IsDeleted = false
                };

                reports.Add(report);

                // 🎯 Her report’a bağlı olarak 0–1 ModerationAction
                if (faker.Random.Bool(0.5f))
                {
                    var actionType = faker.PickRandom("Ban", "Restrict", "Warning");
                    var action = new Domain.Entities.ModerationAction
                    {
                        Id = Guid.NewGuid(),
                        PlayerId = reportedId,
                        ActionType = actionType,
                        Reason = $"Auto-moderation: {reason.Value}",
                        ActionDateUtc = _clock.UtcNow.AddHours(-faker.Random.Int(10, 200)),
                        ExpiryDateUtc = actionType != "Warning" && faker.Random.Bool(0.5f)
                            ? _clock.UtcNow.AddHours(faker.Random.Int(24, 168))
                            : null,
                        ModeratorId = Guid.NewGuid(),
                        IsActive = faker.Random.Bool(0.8f),
                        CreatedAtUtc = _clock.UtcNow,
                        IsDeleted = false
                    };
                    actions.Add(action);
                }
            }

            await _reportRepo.AddRangeAsync(reports);
            await _actionRepo.AddRangeAsync(actions);
            await _reportRepo.SaveAsync();
            await _actionRepo.SaveAsync();

            return Unit.Value;
        }
    }
}
