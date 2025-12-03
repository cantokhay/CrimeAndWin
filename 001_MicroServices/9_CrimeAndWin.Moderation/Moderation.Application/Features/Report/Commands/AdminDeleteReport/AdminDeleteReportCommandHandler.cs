using MediatR;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Commands.AdminDeleteReport
{
    public sealed class AdminDeleteReportCommandHandler
            : IRequestHandler<AdminDeleteReportCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Report> _write;

        public AdminDeleteReportCommandHandler(IWriteRepository<Domain.Entities.Report> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteReportCommand request, CancellationToken cancellationToken)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
