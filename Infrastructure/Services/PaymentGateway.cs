using Domain.Repositories.Services;
using Shared.Helpers;
using Stripe;

namespace Infrastructure.Services;

public class PaymentGateway : IPaymentGateway
{
    public PaymentGateway()
    {
        StripeConfiguration.ApiKey = Appsettings.Instance.GetValue("Stripe:SecretKey");
    }

    public async Task<Charge> CreateChargeAsync(string token, decimal amount, string currency = "usd")
    {
        var options = new ChargeCreateOptions
        {
            Amount = amount.ToEuro(),
            Currency = currency,
            Source = token,
            Description = "Test Charge"
        };

        var service = new ChargeService();
        Charge charge = await service.CreateAsync(options);
        return charge;
    }

    public async Task<Customer> CreateCustomersync(string email, string name, string cardToken)
    {
        var options = new CustomerCreateOptions
        {
            Email = email,
            Name = name,
            Source = cardToken,
        };

        var service = new CustomerService();
        Customer customer = await service.CreateAsync(options);
        return customer;
    }

    public async Task<PaymentIntent> ChargeCustomerAsync(string customerId, long amount, string currency = "usd")
    {
        var options = new PaymentIntentCreateOptions
        {
            Customer = customerId,
            Amount = amount,
            Currency = currency,
            PaymentMethodTypes = new List<string> { "card" },
            OffSession = true, 
            Confirm = true     
        };

        var service = new PaymentIntentService();
        PaymentIntent paymentIntent = await service.CreateAsync(options);
        return paymentIntent;
    }

    public async Task<Card> GetCustomerCardAsync(string customerId, string cardId)
    {
        var service = new CardService();
        Card card = await service.GetAsync(customerId, cardId);
        return card;
    }

    public async Task<List<PaymentMethod>> GetCustomerPaymentMethodsAsync(string customerId)
    {
        var options = new PaymentMethodListOptions
        {
            Customer = customerId,
            Type = "card"
        };

        StripeList<PaymentMethod> paymentMethods = await new PaymentMethodService().ListAsync(options);
        return paymentMethods.Data;
    }


    public async Task<Subscription> Create(string customerId, string planId)
    {
        var options = new SubscriptionCreateOptions
        {
            Customer = customerId,
            Items = new List<SubscriptionItemOptions>
            {
                new SubscriptionItemOptions
                {
                    Plan = planId,
                },
            },
        };

        var service = new SubscriptionService();
        var subscription = await service.CreateAsync(options);
        return subscription;
    }

    public async Task<StripeList<Subscription>> Get()
    {
        var options = new SubscriptionListOptions { Limit = 3 };
        var service = new SubscriptionService();
        StripeList<Subscription> subscriptions = await service.ListAsync(options);
        return subscriptions;
    }

    public async Task<Subscription> Get(string subscriptionId)
    {
        var service = new SubscriptionService();
        var subscription = await service.GetAsync(subscriptionId);
        return subscription;
    }
}
