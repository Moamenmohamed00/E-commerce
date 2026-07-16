using ECommerce.Application.Common.Models;
using MediatR;

namespace ECommerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;

public record RemoveFromWishlistCommand(int Id) : IRequest<Result>;