namespace SampleLMS.Dal.Interfaces
{
    public interface IImageInterface
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
