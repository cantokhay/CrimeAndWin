using MediatR;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Features.Report.Commands
{
    public record CreateReportCommand(CreateReportDTO Dto) : IRequest<Guid>;
}
