using App.Application.Command.File;
using MediatR;

namespace App.Api.Endpoints;

public static class FileEndpoints
{

    public static void MapFileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/file").RequireAuthorization();

        group.MapPost("/upload", async (IMediator mediator, IFormFile file) =>
            {
                var fileUploadCommand = new UploadFileCommand(file.OpenReadStream(), file.FileName);
                var result = await mediator.Send(fileUploadCommand);
                return Results.Ok(result);
            })
            .WithName("UploadFile");
    }
}