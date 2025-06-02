namespace App.Domain.Model.User;

public class UserEntity
{
    public Guid Id { get; private set; } 

    public string UserName { get; private set; } = null!;
    
    public string LastName { get; private set; } = null!;
    
    public string FirstName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

}