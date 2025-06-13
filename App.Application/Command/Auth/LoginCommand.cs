using App.Domain.Model.User;
using MediatR;
namespace App.Application.Command.Auth;

public record LoginResult(string Token);


public record LoginCommand(string Email, string Password) : IRequest<LoginResult>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IUserReadRepository _userReadRepository;

    public LoginCommandHandler(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByEmailAsync(request.Email);
        
        if(user == null)
        {
            throw new Exception("User not found");
        }
        
        user.PasswordHash.VerifyPassword(request.Password);
        
        return new LoginResult("token");
        
    }
}