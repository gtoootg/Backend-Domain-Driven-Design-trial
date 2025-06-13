using App.Application.Command.Auth;
using MediatR;

namespace App.Api.Endpoints;

public static class LoginEndpoints
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/login");
        
        group.MapPost("/", async (IMediator mediator, LoginCommand command) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("Login")
        .WithOpenApi()
        .Produces<LoginResult>(StatusCodes.Status200OK);
    }
}