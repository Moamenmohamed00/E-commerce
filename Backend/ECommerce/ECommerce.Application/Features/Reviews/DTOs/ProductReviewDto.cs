namespace ECommerce.Application.Features.Reviews.DTOs;

public record ProductReviewDto(
    int Id,
    string ReviewerName, 
    int Rating,          
    string Comment,
    DateTime CreatedAt
);