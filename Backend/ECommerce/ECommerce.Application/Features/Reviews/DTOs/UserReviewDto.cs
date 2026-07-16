namespace ECommerce.Application.Features.Reviews.DTOs;

public record UserReviewDto(
    int Id,
    int ProductId,
    string ProductName,
    string? ProductImageUrl, 
    int Rating,
    string Comment,
    DateTime CreatedAt
);