using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymenRepository;
        public PaymentService(IPaymentRepository paymenRepository)
        {
            _paymenRepository = paymenRepository;
        }

        public async Task<PaymentDTO> CreatePayment(Payment payment)
        {
            var paymentDto = payment.Adapt<PaymentDTO>();
            return  await _paymenRepository.CreatePayment(paymentDto);
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            return await _paymenRepository.GetAllPayments();
        }

        public async Task<PaymentDTO> GetPaymentById(string id)
        {
            return await _paymenRepository.GetPaymentById(id);
        }

        public async Task<PaymentDTO> UpdatePaymentById(PaymentDTO payment)
        {
            return await _paymenRepository.UpdatePaymentById(payment);
        }

        public async Task<PaymentDTO> DeletePaymentById(string id)
        {
            return await _paymenRepository.DeletePaymentById(id);
        }
    }
}
