using Microsoft.AspNetCore.Http;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class FileManager : IFileManager
    {

        public ResponseModel<List<FileHandlerResponse>> FileHandlerService(List<IFormFile> model)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var handledFiles = new List<FileHandlerResponse>();

            foreach (var file in model)
            {
                const long MaxFileSize = 5 * 1024 * 1024;

                if (file.Length > MaxFileSize)

                    return new ResponseModel<List<FileHandlerResponse>>
                    {

                        Success = false,
                        Message = "FileSizeLimitExceded"

                    };

                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };

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
                var relativePath = Path.Combine("Uploads", newFileName);

                var physicalPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
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
