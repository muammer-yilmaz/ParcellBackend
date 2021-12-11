using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public interface IBaseRepository<Tmodel> where Tmodel : BaseMongoModel {

        Task<List<Tmodel>> GetList();
        Task<Tmodel> Get(string id);
        Task Create(Tmodel model);
        Task Update(string id, Tmodel model);
        Task Delete(string id);

        
    }
}
