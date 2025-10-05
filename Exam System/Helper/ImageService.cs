using Exam_System.Shared.Interface;

namespace Exam_System.Service
{
    public class ImageService : IImageHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment )
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public Task DeleteImageAsync(string imageUrl)
        {
            //    if (string.IsNullOrEmpty(imageUrl))
            //        return Task.CompletedTask;

            //    // Remove base URL if present
            //    var uri = new Uri(imageUrl, UriKind.RelativeOrAbsolute);
            //    string relativePath = imageUrl;
            //    if (uri.IsAbsoluteUri)
            //    {
            //        relativePath = uri.AbsolutePath.TrimStart('/');
            //    }

            //    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
            //    if (File.Exists(fullPath)) File.Delete(fullPath);
            //    return Task.CompletedTask;
            //
            if (string.IsNullOrEmpty(imageUrl))
                return Task.CompletedTask;
            var uri = new Uri(imageUrl, UriKind.RelativeOrAbsolute);
            string relativePath = imageUrl;
            if (uri.IsAbsoluteUri)
            {
                relativePath = uri.AbsolutePath.TrimStart('/');
            }
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
            
            if (File.Exists(fullPath)) File.Delete(fullPath);
                return Task.CompletedTask;


        }


        public Task<string> UpdateImageAsync(IFormFile file, string imageUrl, string folder)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder)
        {
            //var uploadsFolder = Path.Combine(_env.WebRootPath, folder)
            // ensure folder is exists
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath,folder);
            if(!Directory.Exists(uploadsFolder))
            {
               Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString()+file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
               await file.CopyToAsync(fileStream);
            }
            return   $"{folder}/{uniqueFileName}".Replace("\\", "/");




        }
    }
}
