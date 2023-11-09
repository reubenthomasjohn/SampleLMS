using SampleLMS.Models.S3;

namespace SampleLMS.Services
{
    public interface IStorageServiceInterface
    {
        //Task<S3ResponseDto> UploadFileAsync(S3Object s3obj, AwsCredentials credentials);
        Task<S3ResponseDto> UploadFileAsync(IFormFile file);
    }
}
