using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

public class NotificationDb : IDbModel, IMapFrom<Notification>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ObjectId { get; set; }

    public NotificationType NotificationType { get; set; }

    public bool IsValid { get; set; } = false;
}