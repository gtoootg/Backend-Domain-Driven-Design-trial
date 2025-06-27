namespace App.Domain.Services;

public interface IFileUploader
{
    Task<string> UploadAsync(Stream fileStream, string fileName);
}