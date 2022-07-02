namespace Application.Services.Identity;

public interface ICurrentUserService
{
    int? UserId { get; }
}