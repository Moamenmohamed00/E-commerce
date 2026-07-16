using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Wishlist.Commands.AddToWishlist;

public record AddToWishlistCommand(int VariantId) : IRequest<Result<int>>;