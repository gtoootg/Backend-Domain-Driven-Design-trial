using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Model.Common;
using App.Domain.Model.Country;
using App.Domain.Values;

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
    
    public string PasswordHash { get; set; }

    public void SetPassword(string password)
    {
        Password = password;
    }
        
    public PasswordValue Password
    {
        set => PasswordHash　= value.GetPasswordHash();
    }
}