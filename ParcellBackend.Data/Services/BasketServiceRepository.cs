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

        
        public async Task AddPlanToBasket(string userId, string planId) {

            var UserBasket = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

            if (UserBasket is null) {
                await Create(new Basket {
                    UserId = userId,
                    PlanId = planId,
                });
            }
            else {
                await Update(UserBasket.Id, new Basket {
                    UserId = userId,
                    PlanId = planId
                });
            }
        }

        public async Task<Basket> GetUserBasket(string userId) {
            var basket = await base.modelMongoCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

            if(basket is null) {
                return null;
            }
            else {
                return basket;
            }
        }
    }
}
