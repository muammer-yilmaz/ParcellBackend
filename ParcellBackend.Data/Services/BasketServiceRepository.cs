using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class BasketServiceRepository : BaseMongoRepository<Basket> {


        public BasketServiceRepository(IDbClient<Basket> dbClient) : base(dbClient) {



        }


        public override Task Create(Basket model) {
            return base.Create(model);
        }

        public override Task Delete(string id) {
            return base.Delete(id);
        }

        public override Task<Basket> Get(string id) {
            return base.Get(id);
        }

        public override Task<List<Basket>> GetList() {
            return base.GetList();
        }

        public override Task Update(string id, Basket model) {
            return base.Update(id, model);
        }

        //public async Task<Basket> GetUserBasket(string userId)

        //public async Task<string> GetBasketPlan(string userId) {
        //    var basket = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        //    return basket.PlanId;
        //}
        //public async Task<string> CheckPlan(string userId) {
        //    var basket = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        //    return basket.PlanId;
        //}
        public async Task<List<string>> CheckBasketDevices(string userId) {
            var basket = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return basket.BasketDevices;
        }

        //public async Task AddPlanToBasket(string userId, string planId) {

        //    var filter = Builders<Basket>.Filter.Where(x => x.UserId == userId);
        //    var update = Builders<Basket>.Update.Set(x => x.PlanId, planId);
        //    var options = new FindOneAndUpdateOptions<Basket>();

        //    await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);

        //}

        public async Task AddDeviceToBasket(string userId, string deviceId) {

            var filter = Builders<Basket>.Filter.Where(x => x.UserId == userId);
            var update = Builders<Basket>.Update.Push(x => x.BasketDevices, deviceId);
            var options = new FindOneAndUpdateOptions<Basket>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<Basket> GetUserBasket(string userId) {
            var basket = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

            return basket;

        }
        //public async Task DeleteBasketPlan(string userId) {

        //    var filter = Builders<Basket>.Filter.Where(x => x.UserId == userId);
        //    var update = Builders<Basket>.Update.Set(x => x.PlanId, null);
        //    var options = new FindOneAndUpdateOptions<Basket>();

        //    await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        //}
        public async Task DeleteBasketDevice(string userId, string deviceId) {

            var filter = Builders<Basket>.Filter.Where(x => x.UserId == userId);
            var basket = await base.modelMongoCollection.Find(filter).FirstOrDefaultAsync();
            var BasketDevices = basket.BasketDevices;
            BasketDevices.Remove(deviceId);
            var update = Builders<Basket>.Update.Set(x => x.BasketDevices, BasketDevices);
            var options = new FindOneAndUpdateOptions<Basket>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task ClearBasket(string userId) {
            var filter = Builders<Basket>.Filter.Where(x => x.UserId == userId);
            var basket = await base.modelMongoCollection.Find(filter).FirstOrDefaultAsync();

            var update = Builders<Basket>.Update.Set(x => x.BasketDevices, new List<string>());
            var options = new FindOneAndUpdateOptions<Basket>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

    }
}
