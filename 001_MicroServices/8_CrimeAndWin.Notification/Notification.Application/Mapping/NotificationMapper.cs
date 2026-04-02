using Notification.Application.DTOs;
using Notification.Application.DTOs.Admin;
using Notification.Domain.VOs;
using Riok.Mapperly.Abstractions;

namespace Notification.Application.Mapping;

[Mapper]
public partial class NotificationMapper
{
    // ResultNotificationDTO
    [MapProperty(nameof(Domain.Entities.Notification.Content.Title), nameof(ResultNotificationDTO.Title))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Message), nameof(ResultNotificationDTO.Message))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Type), nameof(ResultNotificationDTO.Type))]
    public partial ResultNotificationDTO ToResultDto(Domain.Entities.Notification entity);
    
    [MapProperty(nameof(ResultNotificationDTO.Title), "Content.Title")]
    [MapProperty(nameof(ResultNotificationDTO.Message), "Content.Message")]
    [MapProperty(nameof(ResultNotificationDTO.Type), "Content.Type")]
    public partial Domain.Entities.Notification ToEntity(ResultNotificationDTO dto);

    // CreateNotificationDTO
    [MapProperty(nameof(Domain.Entities.Notification.Content.Title), nameof(CreateNotificationDTO.Title))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Message), nameof(CreateNotificationDTO.Message))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Type), nameof(CreateNotificationDTO.Type))]
    public partial CreateNotificationDTO ToCreateDto(Domain.Entities.Notification entity);

    [MapProperty(nameof(CreateNotificationDTO.Title), "Content.Title")]
    [MapProperty(nameof(CreateNotificationDTO.Message), "Content.Message")]
    [MapProperty(nameof(CreateNotificationDTO.Type), "Content.Type")]
    public partial Domain.Entities.Notification ToEntity(CreateNotificationDTO dto);

    // UpdateNotificationDTO
    [MapProperty(nameof(Domain.Entities.Notification.Content.Title), nameof(UpdateNotificationDTO.Title))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Message), nameof(UpdateNotificationDTO.Message))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Type), nameof(UpdateNotificationDTO.Type))]
    public partial UpdateNotificationDTO ToUpdateDto(Domain.Entities.Notification entity);

    [MapProperty(nameof(UpdateNotificationDTO.Title), "Content.Title")]
    [MapProperty(nameof(UpdateNotificationDTO.Message), "Content.Message")]
    [MapProperty(nameof(UpdateNotificationDTO.Type), "Content.Type")]
    public partial Domain.Entities.Notification ToEntity(UpdateNotificationDTO dto);

    // GetByIdNotificationDTO
    [MapProperty(nameof(Domain.Entities.Notification.Content.Title), nameof(GetByIdNotificationDTO.Title))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Message), nameof(GetByIdNotificationDTO.Message))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Type), nameof(GetByIdNotificationDTO.Type))]
    public partial GetByIdNotificationDTO ToByIdDto(Domain.Entities.Notification entity);

    [MapProperty(nameof(GetByIdNotificationDTO.Title), "Content.Title")]
    [MapProperty(nameof(GetByIdNotificationDTO.Message), "Content.Message")]
    [MapProperty(nameof(GetByIdNotificationDTO.Type), "Content.Type")]
    public partial Domain.Entities.Notification ToEntity(GetByIdNotificationDTO dto);

    // Admin Mappings
    [MapProperty(nameof(Domain.Entities.Notification.Content.Title), nameof(AdminResultNotificationDTO.Title))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Message), nameof(AdminResultNotificationDTO.Message))]
    [MapProperty(nameof(Domain.Entities.Notification.Content.Type), nameof(AdminResultNotificationDTO.Type))]
    public partial AdminResultNotificationDTO ToAdminResultDto(Domain.Entities.Notification entity);

    [MapProperty(nameof(AdminResultNotificationDTO.Title), "Content.Title")]
    [MapProperty(nameof(AdminResultNotificationDTO.Message), "Content.Message")]
    [MapProperty(nameof(AdminResultNotificationDTO.Type), "Content.Type")]
    public partial Domain.Entities.Notification ToEntity(AdminResultNotificationDTO dto);

    [MapperIgnoreTarget(nameof(Domain.Entities.Notification.Id))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Notification.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Notification.UpdatedAtUtc))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Notification.IsDeleted))]
    [MapProperty(nameof(AdminCreateNotificationDTO.Title), "Content.Title")]
    [MapProperty(nameof(AdminCreateNotificationDTO.Message), "Content.Message")]
    [MapProperty(nameof(AdminCreateNotificationDTO.Type), "Content.Type")]
    public partial Domain.Entities.Notification ToEntity(AdminCreateNotificationDTO dto);

    [MapperIgnoreTarget(nameof(Domain.Entities.Notification.CreatedAtUtc))]
    [MapProperty(nameof(AdminUpdateNotificationDTO.Title), "Content.Title")]
    [MapProperty(nameof(AdminUpdateNotificationDTO.Message), "Content.Message")]
    [MapProperty(nameof(AdminUpdateNotificationDTO.Type), "Content.Type")]
    public partial Domain.Entities.Notification ToEntity(AdminUpdateNotificationDTO dto);

    // Collection Mappings
    public partial IEnumerable<ResultNotificationDTO> ToResultDtoList(IEnumerable<Domain.Entities.Notification> entities);
    public partial IEnumerable<AdminResultNotificationDTO> ToAdminResultDtoList(IEnumerable<Domain.Entities.Notification> entities);
}

