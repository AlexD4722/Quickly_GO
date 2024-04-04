using Microsoft.AspNetCore.Http;

namespace QuicklyGo.Utils
{
    public class FileManager
    {
        private static string[] NotAllowedExtensions = { ".exe", ".dll" };
        private static int MaxSize = 100000000; // bytes
        private static string UploadDir = Directory.GetCurrentDirectory() + "\\wwwroot\\fileupload";
        public static async Task<FileSaveResult> SaveFile(IFormFile file, string directory)
        {
            if (file.Length > 0 && file.Length < MaxSize && !NotAllowedExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(UploadDir, directory);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                using (var stream = System.IO.File.Create(Path.Combine(filePath, fileName)))
                {
                    await file.CopyToAsync(stream);
                }
                return new FileSaveResult { FileName = fileName, FilePath = $"\\fileupload\\{directory}" };
            }
            else
            {
                throw new Exception($"File {file.Name} is not valid");
            }
        }

        public static async Task<FileSaveResult> SavePicture(IFormFile file, string directory)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (file.Length > 0 && file.Length < MaxSize 
                && !NotAllowedExtensions.Contains(Path.GetExtension(file.FileName)) 
                && allowedExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(UploadDir, directory);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                using (var stream = System.IO.File.Create(Path.Combine(filePath, fileName)))
                {
                    await file.CopyToAsync(stream);
                }
                return new FileSaveResult { FileName = fileName, FilePath = $"\\fileupload\\{directory}" };
            }
            else
            {
                throw new Exception($"File {file.Name} is not valid");
            }
        }

        public class FileSaveResult
        {
            public required string FileName { get; set; }
            public required string FilePath { get; set; }
        }
    }
}
