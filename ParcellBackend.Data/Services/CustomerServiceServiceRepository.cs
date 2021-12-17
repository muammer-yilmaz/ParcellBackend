using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services
{
    public class CustomerServiceServiceRepository : BaseMongoRepository<CustomerService>
    {
        public CustomerServiceServiceRepository(IDbClient<CustomerService> dbClient) : base(dbClient){

            }

        public override Task Create(CustomerService model)
        {
            return base.Create(model);
        }

        public override Task Delete(string id)
        {
            return base.Delete(id);
        }

        public override Task<CustomerService> Get(string id)
        {
            return base.Get(id);
        }

        public override Task<List<CustomerService>> GetList()
        {
            return base.GetList();
        }

        public override Task Update(string id, CustomerService model)
        {
            return base.Update(id, model);
        }
    }
}
