using Bogus;
using MediatR;
using Notification.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Notification.Application.Features.Notification.Commands.Seed
{
    public sealed class RunNotificationSeedHandler : IRequestHandler<RunNotificationSeedCommand, Unit>
    {
        private readonly IWriteRepository<Domain.Entities.Notification> _notificationRepo;
        private readonly IDateTimeProvider _clock;

        public RunNotificationSeedHandler(
            IWriteRepository<Domain.Entities.Notification> notificationRepo,
            IDateTimeProvider clock)
        {
            _notificationRepo = notificationRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunNotificationSeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("en");

            var notifications = new List<Domain.Entities.Notification>();

            for (int i = 0; i < request.Count; i++)
            {
                var playerId = Guid.NewGuid();
                var countPerPlayer = faker.Random.Int(2, 5);

                for (int j = 0; j < countPerPlayer; j++)
                {
                    var notif = new Domain.Entities.Notification
                    {
                        Id = Guid.NewGuid(),
                        PlayerId = playerId,
                        Content = new NotificationContent(
                            faker.Hacker.Phrase().Truncate(60),
                            faker.Lorem.Sentence().Truncate(200),
                            faker.PickRandom("Info", "Warning", "Alert", "System")
                        ),
                        CreatedAtUtc = _clock.UtcNow.AddMinutes(-faker.Random.Int(10, 500)),
                        IsDeleted = false
                    };

                    notifications.Add(notif);
                }
            }

            await _notificationRepo.AddRangeAsync(notifications);
            await _notificationRepo.SaveAsync();

            return Unit.Value;
        }

    }
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength)
            => string.IsNullOrEmpty(value) ? value : value.Length <= maxLength ? value : value[..maxLength];
    }
}
