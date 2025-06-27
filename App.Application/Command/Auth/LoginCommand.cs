using App.Application.Auth;
using App.Domain.Model.User;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace App.Application.Command.Auth;

public record LoginResult(string Token, string RefreshToken);

public record LoginCommand(string Email, string Password) : IRequest<LoginResult>;

public class LoginCommandHandler(
    IUserReadRepository userReadRepository, 
    IConfiguration configuration,
    IDistributedCache cache
    ) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByEmailAsync(request.Email);
        
        if(user == null)
        {
            throw new Exception("User not found");
        }
        
        var isLoginSuccess =user.PasswordHash.VerifyPassword(request.Password);

        if (!isLoginSuccess)
        {
            throw new Exception("Login failed");
        }
        
        var secretKey = configuration["Jwt:Key"];
        var token = JwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Email, secretKey);
        var refreshToken = RefreshTokenGenerator.GenerateRefreshToken();
        var cacheKey = $"refresh_{user.Id}_{refreshToken}";
        
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
        };
        
        await cache.SetStringAsync(cacheKey, user.Id.ToString(), options, cancellationToken);
        
        return new LoginResult(token, refreshToken);
    }
}