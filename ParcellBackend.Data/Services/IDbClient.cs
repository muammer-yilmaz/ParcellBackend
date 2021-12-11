using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public interface IDbClient<Tmodel> where Tmodel : BaseMongoModel {
        IMongoCollection<Tmodel> GetMongoCollection();
    }
}
