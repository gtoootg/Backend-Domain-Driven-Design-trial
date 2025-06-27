using App.Application.Queries.Users;
using App.Domain.Model.User;
using MediatR;

namespace App.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users") .RequireAuthorization();
        

        group.MapGet("/", async (IMediator mediator) =>
        {
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetAllUsers")
        .WithOpenApi()
        .Produces<IEnumerable<UserReadModel>>(StatusCodes.Status200OK);
    }
}
