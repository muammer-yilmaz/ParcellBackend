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
        private readonly List<string> numberFirst3Digit =
            new List<string> { "510", "511", "512", "513", "514", "515", "516", "517", "518", "519", "520" };
        private readonly BasketServiceRepository basketService;
        private readonly PlanServiceRepository planService;
        static Random rnd = new Random();
        public UserServiceRepository(IDbClient<User> dbClient, BasketServiceRepository basketService,
            PlanServiceRepository planService) : base(dbClient) {
            this.planService = planService;
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

        public async Task<User> GetUserWithPassword(string password) {
            var user = await base.modelMongoCollection.Find(x => x.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserWithPhone(string phone) {
            var user = await base.modelMongoCollection.Find(x => x.Phone == phone).FirstOrDefaultAsync();
            return user;
        }

        public async Task<string> AssignPhoneNumber() {

            StringBuilder builder = new StringBuilder(12);
            var first3Digit = numberFirst3Digit[rnd.Next(numberFirst3Digit.Count)];
            var second3Digit = rnd.Next(100, 999);
            var digit4First2 = rnd.Next(10, 99);
            var digit4Last2 = rnd.Next(10, 99);
            var fullString = first3Digit + "-" + second3Digit.ToString() + "-" + digit4First2.ToString() + digit4Last2.ToString();
            builder.Append(fullString);

            return builder.ToString();
        }

        public async Task<bool> CheckNumberAvailability(string phoneNumber) {
            var response = await base.modelMongoCollection.Find(x => x.Phone == phoneNumber).FirstOrDefaultAsync();

            if (response is null) {
                return true;
            }
            else
                return false;
        }

        public async Task ChangeUserPassword(string userId, string newPassword) {

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Set(x => x.Password, newPassword);
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task SetUserPlan(string userId, string planId) {
            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Set(x => x.PlanId, planId);
            var options = new FindOneAndUpdateOptions<User>();
            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
            await SetRemaingUsage(userId,planId);
        }

        public async Task CreateUserBasket(string mail) {
            var user = await base.modelMongoCollection.Find(x => x.Mail == mail).FirstOrDefaultAsync();

            await basketService.Create(new Basket {
                UserId = user.Id,
                BasketDevices = new List<string>()
            });
        }

        public async Task UpdateUserInfo(string userId, string mail, string address) {

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Combine(Builders<User>.Update.Set(x => x.Mail, mail),
                Builders<User>.Update.Set(x => x.Address, address));
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<string> GetUserPlan(string userId) {

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var user = await base.modelMongoCollection.Find(filter).FirstOrDefaultAsync();

            return user.PlanId;

        }

        public async Task<double> GetUserBalance(string userId) {
            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var user = await base.modelMongoCollection.Find(filter).FirstOrDefaultAsync();

            return user.Balance;
        }

        public async Task UpdateUserBalance(string userId, double newBalance) {

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);

            var update = Builders<User>.Update.Set(x => x.Balance, newBalance);
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task PasswordForget(string mail, string newPassword) {

            var filter = Builders<User>.Filter.Where(x => x.Mail == mail);
            var update = Builders<User>.Update.Set(x => x.Password, newPassword);
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);

        }

        public async Task<RemaingUsage> GetRemaingUsage(string userId) {
            var user = await Get(userId);

            return user.RemaingUsage;

        }

        public async Task SetRemaingUsage(string userId, string planId) {

            var plan = await planService.Get(planId);

            var usage = new RemaingUsage {
                Internet = plan.Internet,
                Minutes = plan.Minutes,
                Sms = plan.Sms,
            };

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Set(x => x.RemaingUsage, usage);
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task ChangeRemaingUsage(string userId, RemaingUsage remaingUsage) {
            var user = await Get(userId);

            var filter = Builders<User>.Filter.Where(x => x.Id == userId);
            var update = Builders<User>.Update.Set(x => x.RemaingUsage, remaingUsage);
            var options = new FindOneAndUpdateOptions<User>();

            await base.modelMongoCollection.FindOneAndUpdateAsync(filter, update, options);
        }
    }
}
