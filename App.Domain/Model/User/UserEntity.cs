using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Model.Common;
using App.Domain.Model.Country;

namespace App.Domain.Model.User;

public class UserEntity: BaseEntity
{
    public string UserName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;
    
    public int CountryId { get; set; }
    
    public CountryEntity Country { get; set; } = null!;
    
    public string CountryCode => Country.CountryCode;
}