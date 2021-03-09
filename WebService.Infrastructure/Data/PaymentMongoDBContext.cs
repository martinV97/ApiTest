using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Data
{
    public class PaymentMongoDBContext : IPaymentMongoDBContext
    {
        private readonly IMongoCollection<PaymentDTO> _mongoDatabase;
        public PaymentMongoDBContext(IMongoDatabase mongoDatabase, IOptions<Collections> settings)
        {
            _mongoDatabase = mongoDatabase.GetCollection<PaymentDTO>(settings.Value.PaymentCollectionName);
        }

        public async Task<PaymentDTO> CreatePayment(PaymentDTO payment)
        {
            await _mongoDatabase.InsertOneAsync(payment);
            return await _mongoDatabase.FindAsync("{}", new FindOptions<PaymentDTO>() { Sort = Builders<PaymentDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            return await _mongoDatabase.FindAsync("{}", new FindOptions<PaymentDTO>() { Sort = Builders<PaymentDTO>.Sort.Descending("_id") }).Result.ToListAsync();
        }

        public async Task<PaymentDTO> GetPaymentById(string id)
        {
            return await _mongoDatabase.FindAsync(p => p.Id.Equals(ObjectId.Parse(id)), 
                new FindOptions<PaymentDTO>() { Sort = Builders<PaymentDTO>.Sort.Descending("_id") }).Result.FirstOrDefaultAsync();
        }

        public async Task<PaymentDTO> UpdatePaymentById(PaymentDTO payment)
        {
            await _mongoDatabase.ReplaceOneAsync(p => p.Id.Equals(payment.Id), payment);
            return payment;
        }

        public async Task DeletePaymentById(string id)
        {
            await _mongoDatabase.DeleteOneAsync(p => p.Id.Equals(ObjectId.Parse(id)));
        }
    }
}
