using Amazon.S3;
using Amazon.S3.Transfer;
using App.Domain.Services;

namespace App.Infrastructure.Services;


public class S3FileUploader: IFileUploader
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    
    public S3FileUploader(IAmazonS3 s3Client, string bucketName)
    {
        _s3Client = s3Client;
        _bucketName = bucketName;
    }
    
    public async Task< UploadFileAsyncResponse> UploadAsync(Stream fileStream, string fileName)
    {
        var key = Guid.NewGuid().ToString();
        
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            Key = key,
            BucketName = _bucketName
        };
        
        var transferUtility = new TransferUtility(_s3Client);
        await transferUtility.UploadAsync(uploadRequest);

        return new UploadFileAsyncResponse(key, fileName);
    }
}