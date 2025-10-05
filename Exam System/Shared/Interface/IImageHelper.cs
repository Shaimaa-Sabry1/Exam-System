namespace Exam_System.Shared.Interface
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile file, string  folder);
        Task DeleteImageAsync(string imageUrl);
        Task<string> UpdateImageAsync(IFormFile file, string imageUrl, string folder);

    }
}
