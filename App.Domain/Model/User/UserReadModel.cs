using App.Domain.Values;

namespace App.Domain.Model.User;

public record UserReadModel(
    int Id,
    string UserName,
    string LastName,
    string FirstName,
    string Email,
    PasswordValue PasswordHash
    )
{
    public static UserReadModel FromEntity(UserEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new UserReadModel(
            entity.Id,
            entity.UserName,
            entity.LastName,
            entity.FirstName,
            entity.Email,
            entity.PasswordHash
            );
    }
}
