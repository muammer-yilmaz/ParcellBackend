using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class UserService {

        private readonly IMongoCollection<User> _userCollection;

        public UserService(IDbClient<User> dbClient) {
            _userCollection = dbClient.GetMongoCollection();
        }

        public async Task<List<User>> GetUsers() =>
            await _userCollection.Find(s => true).ToListAsync();

        public async Task<User?> GetUser(string id) =>
            await _userCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task CreateUser(User user) =>
            await _userCollection.InsertOneAsync(user);

        public async Task UpdateUser(string id, User updatedUser) =>
            await _userCollection.ReplaceOneAsync(s => s.Id == id , updatedUser);

        public async Task DeleteUser(string id) =>
            await _userCollection.DeleteOneAsync(s => s.Id == id);

        public async Task<User?> getUserWithMail(string mail) =>
            await _userCollection.Find(s => s.Mail == mail).FirstOrDefaultAsync();

        public async Task<User?> loginWithMail(string mail, string password) =>
            await _userCollection.Find(s => s.Mail == mail && s.Password == password).FirstOrDefaultAsync();
    }
}
