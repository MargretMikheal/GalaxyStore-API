using Microsoft.AspNetCore.Http;
using System.IO;

namespace GalaxyStore.Core.Helper
{
    public static class FileHelper
    {
        private const string BasePath = "wwwroot/images";

        public static async Task<string> SaveFileAsync(IFormFile file, string subFolder)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), BasePath, subFolder);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(directoryPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{BasePath}/{subFolder}/{fileName}".Replace("\\", "/");
        }

        public static byte[] ReadFileAsBytes(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), filePath.TrimStart('/'));

            if (!File.Exists(absolutePath))
                return null;

            return File.ReadAllBytes(absolutePath);
        }
    }
}
