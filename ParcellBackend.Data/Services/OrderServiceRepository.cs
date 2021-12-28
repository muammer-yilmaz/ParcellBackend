using MongoDB.Bson;
using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class OrderServiceRepository : BaseMongoRepository<Order> {


        public OrderServiceRepository(IDbClient<Order> dbClient) : base(dbClient) {

        }

        public override Task Create(Order model) {
            return base.Create(model);
        }

        public override Task Delete(string id) {
            return base.Delete(id);
        }

        public override Task<Order> Get(string id) {
            return base.Get(id);
        }

        public override Task<List<Order>> GetList() {
            return base.GetList();
        }

        public override Task Update(string id, Order model) {
            return base.Update(id, model);
        }

        public async Task CreateUserOrder(string userId) {
            await base.modelMongoCollection.InsertOneAsync(
                new Order { UserId = userId, OrderItems = new List<OrderItem>() });
        }

        public async Task AddOrder(string userId,OrderItem order) {

            var user = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user is null)
                await CreateUserOrder(userId);

            var filter = Builders<Order>.Filter.Where(x => x.UserId == userId);
            var date = DateTime.Now;
            date = date.AddHours(3);
            var item = new OrderItem { Id = ObjectId.GenerateNewId().ToString(),
                DeviceIds = order.DeviceIds, OrderAddress = order.OrderAddress,
                OrderDate = date,
                TotalPrice = order.TotalPrice };
            var update = Builders<Order>.Update.Push(x => x.OrderItems, item);
            var options = new FindOneAndUpdateOptions<Order>();
            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);

        }

        public async Task<List<OrderItem>> GetUserOrders(string userId) {

            var filter = Builders<Order>.Filter.Where(x => x.UserId == userId);
            var order = await base.modelMongoCollection.Find(filter).FirstOrDefaultAsync();

            var list = order.OrderItems;

            return list;
        }

    }
}
