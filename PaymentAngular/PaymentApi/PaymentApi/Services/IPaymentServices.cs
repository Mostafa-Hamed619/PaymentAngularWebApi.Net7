using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using PaymentApi.Models.Data;

namespace PaymentApi.Services
{
    public interface IPaymentServices
    {
        Task<IEnumerable<PaymentDetails>> GetAllAsync();

        Task<PaymentDetails> GetAsync(int id);

        Task<PaymentDetails> UpdateAsync(int id,PaymentDetails paymentDetails);

        Task<PaymentDetails> AddAsync(PaymentDetails paymentDetails);

        Task DeleteAsync(int id);
    }
}
