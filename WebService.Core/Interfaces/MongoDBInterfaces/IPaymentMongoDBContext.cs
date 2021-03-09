using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IPaymentMongoDBContext
    {
        Task<PaymentDTO> CreatePayment(PaymentDTO payment);
        Task<List<PaymentDTO>> GetAllPayments();
        Task<PaymentDTO> GetPaymentById(string id);
        Task<PaymentDTO> UpdatePaymentById(PaymentDTO payment);
        Task DeletePaymentById(string id);
    }
}
