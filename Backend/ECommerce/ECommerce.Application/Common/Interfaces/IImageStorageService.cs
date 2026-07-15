namespace ECommerce.Application.Common.Interfaces
{
    public interface IImageStorageService
    {
       // Task<string> UploadImageAsync(byte[] imageBytes, string fileName, string contentType, string folder,CancellationToken cancellationToken=default);//keep whole file in ram before send to CDN
       Task<string> UploadImageAsync(Stream fileStream, string fileName, string contentType, string folder,CancellationToken cancellationToken=default);// can convert byte[]->stream not reverse
        Task<string> GetImageAsync(string fileUrl,CancellationToken cancellationToken=default);
        Task<bool> DeleteImageAsync(string fileUrl,CancellationToken cancellationToken=default);
    }
}