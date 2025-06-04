namespace App.Domain.Model.User;

public record UserReadModel(
    Guid Id,
    string UserName,
    string LastName,
    string FirstName,
    string Email)
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
            entity.Email);
    }
}
