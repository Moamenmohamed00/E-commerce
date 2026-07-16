namespace ECommerce.Application.Features.Admin.Dashboard.DTOs;

public record DashboardSummaryDto(
    int TotalUsers,
    int TotalProducts,
    int TotalOrders,
    decimal TotalRevenue,
    int PendingOrdersCount,
    List<RecentOrderDto> RecentOrders
);