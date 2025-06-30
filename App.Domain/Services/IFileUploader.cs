namespace App.Domain.Services;

public record UploadFileAsyncResponse(
    string Key,
    string FileName
);

public interface IFileUploader
{
    Task<UploadFileAsyncResponse> UploadAsync(Stream fileStream, string fileName);
}