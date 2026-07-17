using ECommerce.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ECommerce.Infrastructure.Services;

public class CloudinaryImageService : IImageStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageService(IConfiguration configuration)
    {
        var account = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]
        );

        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string contentType, string folder, CancellationToken cancellationToken = default)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("File stream cannot be empty.");

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            Folder = folder, 
            Transformation = new Transformation().Quality("auto").FetchFormat("auto")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        if (uploadResult.Error != null)
        {
            throw new Exception($"Cloudinary Upload Error: {uploadResult.Error.Message}");
        }

        return uploadResult.SecureUrl.ToString();
    }

    public async Task<string> GetImageAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        
        return await Task.FromResult(fileUrl);
    }

    public async Task<bool> DeleteImageAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileUrl)) return false;

        var publicId = ExtractPublicIdFromUrl(fileUrl);
        
        if (string.IsNullOrEmpty(publicId)) return false;

        var deletionParams = new DeletionParams(publicId)
        {
            ResourceType = ResourceType.Image
        };

        var result = await _cloudinary.DestroyAsync(deletionParams);

        return result.Result == "ok";
    }
  
    private string ExtractPublicIdFromUrl(string url)
    {
        //EX:https://res.cloudinary.com/demo/image/upload/v1234567890/products/my-image.jpg
        
        try
        {
            int uploadIndex = url.IndexOf("upload/");
            if (uploadIndex == -1) return null;

            string afterUpload = url.Substring(uploadIndex + 7);

            if (afterUpload.StartsWith("v") && afterUpload.Contains("/"))
            {
                afterUpload = afterUpload.Substring(afterUpload.IndexOf('/') + 1);
            }

            int lastDotIndex = afterUpload.LastIndexOf('.');
            if (lastDotIndex != -1)
            {
                afterUpload = afterUpload.Substring(0, lastDotIndex);
            }

            return afterUpload; 
        }
        catch
        {
            return null;
        }
    }
}