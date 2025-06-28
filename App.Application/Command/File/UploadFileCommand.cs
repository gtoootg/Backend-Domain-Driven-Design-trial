using App.Domain.Services;
using MediatR;

namespace App.Application.Command.File;

public record UploadFileResult(string FileName);

public record UploadFileCommand(Stream FileStream, string FileName) : IRequest<UploadFileResult>;

public class UploadFileCommandHandler(IFileUploader fileUploader) : IRequestHandler<UploadFileCommand, UploadFileResult>
{
    public async Task<UploadFileResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var result=  await fileUploader.UploadAsync(request.FileStream, request.FileName);
        
        return new UploadFileResult(result);
    }
}