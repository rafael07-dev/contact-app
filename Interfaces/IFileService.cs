namespace ContactApp.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> GetFileName();
    }
}
