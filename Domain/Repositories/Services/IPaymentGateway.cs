using Stripe;

namespace Domain.Repositories.Services;

public interface IPaymentGateway
{
    Task<Charge> CreateChargeAsync(string token, decimal amount, string currency = "usd");
    Task<Customer> CreateCustomersync(string email, string name, string cardToken);
    Task<Card> GetCustomerCardAsync(string customerId, string cardId);
    Task<List<PaymentMethod>> GetCustomerPaymentMethodsAsync(string customerId);
}
