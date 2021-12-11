using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class DbClient<Tmodel> : IDbClient<Tmodel> where Tmodel : BaseMongoModel {

        private readonly IMongoCollection<Tmodel> mongoCollection;
        private readonly IConfiguration _config;

        public DbClient(IOptions<ParcellDatabaseSettings> options, IConfiguration configuration) {

            _config = configuration;

            var client = new MongoClient(options.Value.DbConnectionString);

            var database = client.GetDatabase(options.Value.DatabaseName);

            var obj = options.Value.BaseCollectionName + typeof(Tmodel).Name;

            mongoCollection = database.GetCollection<Tmodel>(_config.GetSection(typeof(ParcellDatabaseSettings).Name)
                .GetSection(obj).Value);

        }

        public IMongoCollection<Tmodel> GetMongoCollection() => mongoCollection;

    }
}
