using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;

namespace App.Domain.Values;



public class PasswordValue: ValueObject
{
    private static int _bCryptWorkFactor = 13;
    private string Password { get; }
    private string PasswordHash { get; }
    
    private PasswordValue(string password)
    {
        // if (!new PasswordSpecification().IsSatisfiedBy(password)) throw new ValidationException("Password is invalid");

        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password.Trim(), workFactor: _bCryptWorkFactor);
        Password = password;
    }
    
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(Password, PasswordHash);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PasswordHash;
    }
    internal string GetPasswordHash()
    {
        return PasswordHash;
    }
    
    public static implicit operator PasswordValue(string password) => new(password);
}