using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Repositories
{

    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPaymentMongoDBContext _paymentMongoDbContext;
        public PaymentRepository(IPaymentMongoDBContext paymenMongoDbContext)
        {
            _paymentMongoDbContext = paymenMongoDbContext;
        }

        public async Task<PaymentDTO> CreatePayment(PaymentDTO payment)
        {
            return  await _paymentMongoDbContext.CreatePayment(payment);
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            return await _paymentMongoDbContext.GetAllPayments();
        }

        public async Task<PaymentDTO> GetPaymentById(string id)
        {
            return await _paymentMongoDbContext.GetPaymentById(id);
        }

        public async Task<PaymentDTO> UpdatePaymentById(PaymentDTO payment)
        {
            return await _paymentMongoDbContext.UpdatePaymentById(payment);
        }

        public async Task<PaymentDTO> DeletePaymentById(string id)
        {
            var paymentDeleted = await GetPaymentById(id);
            await _paymentMongoDbContext.DeletePaymentById(id);
            return paymentDeleted;
        }
    }
}
