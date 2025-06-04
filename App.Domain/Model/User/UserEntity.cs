using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Model.User;

public class UserEntity
{
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; } 

    public string UserName { get; private set; } = null!;
    
    public string LastName { get; private set; } = null!;
    
    public string FirstName { get; private set; } = null!;

    public string Email { get; private set; } = null!;
}