using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Features.ProductImages.Commands.AddProductImage;
using ECommerce.Application.Features.ProductImages.Commands.DeleteProductImage;
using ECommerce.Application.Features.ProductImages.Commands.SetPrimaryProductImage;
using ECommerce.Application.Features.ProductImages.Queries.GetProductImagesByProductId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/products/{productId}/images")]
public class ProductImagesController : ApiControllerBase
{
    private readonly IImageStorageService _imageStorageService;

    public ProductImagesController(IImageStorageService imageStorageService)
    {
        _imageStorageService = imageStorageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductImages(int productId)
    {
        var result = await Mediator.Send(new GetProductImagesByProductIdQuery(productId));
        return HandleResult(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProductImage(int productId, IFormFile file, [FromForm] bool isPrimary = false)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty");
        }

        using var stream = file.OpenReadStream();
        var imageUrl = await _imageStorageService.UploadImageAsync(stream, file.FileName, file.ContentType, $"products/{productId}");

        var publicId = ExtractPublicIdFromUrl(imageUrl);
        var result = await Mediator.Send(new AddProductImageCommand(productId, imageUrl, publicId ?? "", isPrimary));
        
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProductImage(int productId, int id)
    {
        // Note: The command only deletes from DB. Cloudinary deletion could be added in the handler.
        var result = await Mediator.Send(new DeleteProductImageCommand(id, productId));
        return HandleResult(result);
    }

    [HttpPut("{id}/primary")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SetPrimaryImage(int productId, int id)
    {
        var result = await Mediator.Send(new SetPrimaryProductImageCommand(id, productId));
        return HandleResult(result);
    }

    private string? ExtractPublicIdFromUrl(string url)
    {
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
