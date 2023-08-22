using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Models.Data;

namespace PaymentApi.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly AppDbContext db;

        public PaymentServices(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<PaymentDetails> AddAsync(PaymentDetails paymentDetails)
        {
            await db.paymentDetails.AddAsync(paymentDetails);
            await db.SaveChangesAsync();
            return paymentDetails;
        }
        
        public async Task DeleteAsync(int id)
        {
            var result = await db.paymentDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                db.paymentDetails.Remove(result);
                await db.SaveChangesAsync();
                
            }
        }

        public async Task<IEnumerable<PaymentDetails>> GetAllAsync()
        {
            var result = await db.paymentDetails.ToListAsync();
            return result;
        }

        public async Task<PaymentDetails> GetAsync(int id)
        {
            var result = await db.paymentDetails.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<PaymentDetails> UpdateAsync(int id, PaymentDetails paymentDetails)
        {
            var result = await db.paymentDetails.FirstOrDefaultAsync(x => x.Id == id);
            result.cardNumber = paymentDetails.cardNumber;
            result.expiredDate = paymentDetails.expiredDate;
            result.cardSecretNumber = paymentDetails.cardSecretNumber;
            result.cardOwnerName = paymentDetails.cardOwnerName;
            db.paymentDetails.Update(result);
            await db.SaveChangesAsync();
            return result;
        }
    }
}
