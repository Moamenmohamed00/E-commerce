namespace ECommerce.Application.Features.Wishlist.DTOs;

public record WishlistItemDto(
    int Id,
    int VariantId,
    int ProductId,
    string ProductName,
    string Color,
    string Size,
    decimal Price,
    bool IsAvailable,
    string? PrimaryImageUrl
);