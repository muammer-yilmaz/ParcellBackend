using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class InvoiceServiceRepository : BaseMongoRepository<Invoice> {


        public InvoiceServiceRepository(IDbClient<Invoice> dbClient) : base(dbClient) {


        }
        public override Task Create(Invoice model) {
            return base.Create(model);
        }

        public override Task Delete(string id) {
            return base.Delete(id);
        }

        public override Task<Invoice> Get(string id) {
            return base.Get(id);
        }

        public override Task<List<Invoice>> GetList() {
            return base.GetList();
        }

        public override Task Update(string id, Invoice model) {
            return base.Update(id, model);
        }

        public async Task<Invoice> GetInvoice(string userId) {
            var invoice = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

            if (invoice is null) {
                return null;
            }
            else
                return invoice;
        }

        public async Task CreateInvoice(string userId, string planId, string contractTime) {

            var invoice = new Invoice {
                UserId = userId,
                PlanId = planId,
                ContractTime = contractTime,
                ContractDate = DateTime.Now
            };

            await base.modelMongoCollection.InsertOneAsync(invoice);
        }

        public async Task AddInvoice(Invoice invoice, string planId, string contractTime) {

            var filter = Builders<Invoice>.Filter.Where(x => x.UserId == invoice.UserId);
            var update = Builders<Invoice>.Update.Combine(
                Builders<Invoice>.Update.Set(x => x.PlanId, planId),
                Builders<Invoice>.Update.Set(x => x.ContractTime, contractTime));
                Builders<Invoice>.Update.Set(x => x.ContractDate, DateTime.Now);
            var options = new FindOneAndUpdateOptions<Invoice>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task CancelInvoice(string userId) {
            var filter = Builders<Invoice>.Filter.Where(x => x.UserId == userId);
            await base.modelMongoCollection.FindOneAndDeleteAsync(filter);
        }
    }
}
