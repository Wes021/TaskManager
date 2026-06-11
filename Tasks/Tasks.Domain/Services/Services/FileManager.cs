using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class FileManager : IFileManager
    {

        public List<ResponseModel<List<FileHandlerResponse>>> FileHandlerService(List<FileRequestDTO> model)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var handledFiles = new List<FileHandlerResponse>();

            foreach (var file in model)
            {
                const long MaxFileSize = 5 * 1024 * 1024;

                if (file.File.Length > MaxFileSize)
                    throw new ValidationException("File size exceeds limit.");

                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };

                var extension = Path.GetExtension(file.File.FileName)
                    .ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                    throw new ValidationException("Invalid file type.");

                var fileName = Path.GetFileName(file.File.FileName);

                if (fileName.Count(c => c == '.') > 1)
                    throw new ValidationException("File name contains multiple extensions.");

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

            return new List<ResponseModel<List<FileHandlerResponse>>>
    {
        new ResponseModel<List<FileHandlerResponse>>
        {
            Data = handledFiles
        }
    };
        }
    }
}
