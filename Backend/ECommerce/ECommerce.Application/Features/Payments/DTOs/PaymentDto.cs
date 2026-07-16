namespace ECommerce.Application.Features.Payments.DTOs;

public record PaymentDto(
    int Id,
    int OrderId,
    decimal Amount,
    string PaymentStatus, 
    string PaymentMethod,
    string TransactionId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);