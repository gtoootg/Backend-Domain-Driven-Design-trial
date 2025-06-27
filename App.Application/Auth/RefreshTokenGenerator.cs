using System;
using System.Security.Cryptography;

namespace App.Application.Auth;

public static class RefreshTokenGenerator
{
    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
