using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Infrastructure.Services
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public FileManager(IWebHostEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        public ResponseModel<List<FileHandlerResponse>> FileHandlerService(List<IFormFile> model)
        {
            var uploadFolder = _configuration["FileStorage:UploadFolder"];

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var handledFiles = new List<FileHandlerResponse>();

            foreach (var file in model)
            {
                var maxFileSizeMB = _configuration.GetValue<int>(
    "FileStorage:MaxFileSizeMB");

                var maxFileSize = maxFileSizeMB * 1024 * 1024;

                if (file.Length > maxFileSize)

                    return new ResponseModel<List<FileHandlerResponse>>
                    {

                        Success = false,
                        Message = "FileSizeLimitExceded"

                    };

                var allowedExtensions = _configuration
    .GetSection("FileStorage:AllowedExtensions")
    .Get<string[]>();

                var extension = Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                    return new ResponseModel<List<FileHandlerResponse>>
                    {
                        Success = false,
                        Message = "InvalidFileType"

                    };

                var fileName = Path.GetFileName(file.FileName);

                if (fileName.Count(c => c == '.') > 1)
                    return new ResponseModel<List<FileHandlerResponse>>
                    {
                        Success = false,
                        Message = "FileContiansDoubleExtentions"

                    };

                var newFileName = $"{Guid.NewGuid()}{extension}";

                var relativePath = Path.Combine(
    uploadFolder,
    newFileName);

                var physicalPath = Path.Combine(
                    _environment.ContentRootPath,
                    relativePath);

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                handledFiles.Add(new FileHandlerResponse
                {
                    FileType = extension,
                    FileName = newFileName,
                    FilePath = relativePath,
                    FileSize = $"{(double)file.Length / (1024 * 1024):0.##} MB"
                });
            }

            return new ResponseModel<List<FileHandlerResponse>>
            {

                Success = true,
                Data = handledFiles

            };
        }
    }
}
