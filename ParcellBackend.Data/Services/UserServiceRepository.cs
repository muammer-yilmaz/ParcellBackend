using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class UserServiceRepository : BaseMongoRepository<User> {

        //private readonly IMongoCollection<User>
        private readonly List<int> numberFirst3Digit;
        private readonly BasketServiceRepository basketService;
        public UserServiceRepository(IDbClient<User> dbClient, BasketServiceRepository basketService) : base(dbClient) {
            this.basketService = basketService;
        }


        public override Task<List<User>> GetList() {
            return base.GetList();
        }

        public override Task<User> Get(string id) {
            return base.Get(id);
        }

        public override async Task Create(User model) {
            await base.Create(model);
            await CreateUserBasket(model.Mail);
        }

        public override Task Update(string id, User model) {
            return base.Update(id, model);
        }

        public override Task Delete(string id) {
            return base.Delete(id);
        }

        public async Task<User> LoginWithMail(string mail, string password) =>
            await base.modelMongoCollection.Find(x => x.Mail == mail && x.Password == password).FirstOrDefaultAsync();

        public async Task<User> GetUserWithMail(string mail) =>
            await base.modelMongoCollection.Find(x => x.Mail == mail).FirstOrDefaultAsync();

        /*
        public async Task<string> AssignPhoneNumber() {
        }
        */

        public async Task<string> ChangeUserPassword(string oldPassword, string newPassword) {
            var user = await base.modelMongoCollection.Find(x => x.Password == oldPassword).FirstOrDefaultAsync();

            if (user is null) {
                return "NotFound";
            }
            else {
                var changedUser = new User {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Mail = user.Mail,
                    Phone = user.Phone,
                    BirthDate = user.BirthDate,
                    BirthPlace = user.BirthPlace,
                    Address = user.Address,
                    Balance = user.Balance,
                    Gender = user.Gender,
                    Password = newPassword
                };
                await base.modelMongoCollection.ReplaceOneAsync(x => x.Id == user.Id, changedUser);
            }
            return "Ok";

        }
        
        public async Task SetUserPlan(string userId , string planId) {
            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Set(x => x.PlanId, planId);
            var options = new FindOneAndUpdateOptions<User>();
            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task CreateUserBasket(string mail) {
            var user = await base.modelMongoCollection.Find(x => x.Mail == mail).FirstOrDefaultAsync();

            await basketService.Create(new Basket {
                UserId = user.Id,
                PlanId = "",
                BasketDevices = new List<string>()
            });
        }
        public async Task UpdateUserMail(string userId, string mail)
        {

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Set(x => x.Mail, mail);
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }
        public async Task UpdateUserAddress(string userId, string address)
        {

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Set(x => x.Address, address);
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

    }
}
