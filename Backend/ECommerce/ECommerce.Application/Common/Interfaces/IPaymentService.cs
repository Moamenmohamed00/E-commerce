namespace ECommerce.Application.Common.Interfaces
{
    public interface IPaymentService
    {
       Task<string> CreatePaymentTransactionAsync(int orderId, decimal amount, string customerEmail,string currency="EGP",CancellationToken cancellationToken=default);

       Task<bool> VerifyPaymentAsync(string paymentIntentId,CancellationToken cancellationToken=default);
    }
}