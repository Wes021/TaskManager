using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class FileManager : IFileManager
    {

        public ResponseModel<List<FileHandlerResponse>> FileHandlerService(List<FileRequestDTO> model)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var handledFiles = new List<FileHandlerResponse>();

            foreach (var file in model)
            {
                const long MaxFileSize = 5 * 1024 * 1024;

                if (file.File.Length > MaxFileSize)

                    return new ResponseModel<List<FileHandlerResponse>>
                    {

                        Success = false,
                        Message = "FileSizeLimitExceded"

                    };

                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };

                var extension = Path.GetExtension(file.File.FileName)
                    .ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                    return new ResponseModel<List<FileHandlerResponse>>
                    {
                        Success = false,
                        Message = "InvalidFileType"

                    };

                var fileName = Path.GetFileName(file.File.FileName);

                if (fileName.Count(c => c == '.') > 1)
                    return new ResponseModel<List<FileHandlerResponse>>
                    {
                        Success = false,
                        Message = "FileContiansDoubleExtentions"

                    };

                var newFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadFolder, newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.File.CopyTo(stream);
                }

                handledFiles.Add(new FileHandlerResponse
                {
                    FileType = extension,
                    FileName = newFileName,
                    FilePath = filePath
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
