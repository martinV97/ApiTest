using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDTO> CreatePayment(Payment payment);
        Task<List<PaymentDTO>> GetAllPayments();
        Task<PaymentDTO> GetPaymentById(string id);
        Task<PaymentDTO> UpdatePaymentById(PaymentDTO payment);
        Task<PaymentDTO> DeletePaymentById(string id);
    }
}
