using ContactApp.Interfaces;
using System.Runtime.ConstrainedExecution;

namespace ContactApp.Services
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly long _maxFileSize = 10 * 1024 * 1024;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };


        public FileService(IWebHostEnvironment env)
        {
            _webHostEnvironment = env;
        }

        public Task<string> GetFileName()
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) 
                throw new Exception("No file uploaded");

            if (file.Length > _maxFileSize)
                throw new Exception("File size exceeds the 2 MB limit.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if(!_allowedExtensions.Contains(extension))
                throw new Exception($"{extension} is not allowed.");

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{uniqueFileName}";
        }
    }
}
