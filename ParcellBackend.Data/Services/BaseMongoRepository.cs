using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class BaseMongoRepository<Tmodel> : IBaseRepository<Tmodel>
        where Tmodel : BaseMongoModel {

        private readonly IMongoCollection<Tmodel> modelMongoCollection;

        public BaseMongoRepository(IDbClient<Tmodel> dbClient) {

            modelMongoCollection = dbClient.GetMongoCollection();
        }

        public async virtual Task<List<Tmodel>> GetList() =>
            await modelMongoCollection.Find(s => true).ToListAsync();

        public async virtual Task<Tmodel?> Get(string id) =>
            await modelMongoCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async virtual Task Create(Tmodel model) =>
            await modelMongoCollection.InsertOneAsync(model);


        public async virtual Task Update(string id, Tmodel model) =>
            await modelMongoCollection.ReplaceOneAsync(s => s.Id == id, model);

        public async virtual Task Delete(string id) =>
            await modelMongoCollection.DeleteOneAsync(s => s.Id == id);

    }
}
