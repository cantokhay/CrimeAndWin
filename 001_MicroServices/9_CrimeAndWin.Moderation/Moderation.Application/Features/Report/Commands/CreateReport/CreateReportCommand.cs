using MediatR;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Features.Report.Commands.CreateReport
{
    public record CreateReportCommand(CreateReportDTO Dto) : IRequest<Guid>;
}
