namespace ECommerce.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public int? UserId { get; }
        public string Email { get; }
        public string Role { get; }
        public bool IsAuthenticated { get; }
    }
}