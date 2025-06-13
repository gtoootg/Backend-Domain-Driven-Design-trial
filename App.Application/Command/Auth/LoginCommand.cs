using App.Application.Auth;
using App.Domain.Model.User;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace App.Application.Command.Auth;

public record LoginResult(string Token);


public record LoginCommand(string Email, string Password) : IRequest<LoginResult>;

public class LoginCommandHandler(IUserReadRepository userReadRepository, IConfiguration configuration) : IRequestHandler<LoginCommand, LoginResult>
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
        return new LoginResult(token);
    }
}