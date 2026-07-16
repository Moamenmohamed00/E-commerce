using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.AddToCart;

public record AddToCartCommand(int VariantId, int Quantity) : IRequest<Result<int>>;