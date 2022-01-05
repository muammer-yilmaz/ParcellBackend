using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services
{
    public class PlanServiceRepository : BaseMongoRepository<Plan>
    {
        public PlanServiceRepository(IDbClient<Plan> dbClient) : base(dbClient)
        {

        }

        public override Task Create(Plan model)
        {
            return base.Create(model);
        }

        public override Task Delete(string id)
        {
            return base.Delete(id);
        }

        public override Task<Plan> Get(string id)
        {
            return base.Get(id);
        }

        public override Task<List<Plan>> GetList()
        {
            return base.GetList();
        }

        public override Task Update(string id, Plan model)
        {
            return base.Update(id, model);
        }

        public async Task<List<Plan>> GetInvoicePlans() {
            var list = await base.modelMongoCollection.Find(x => x.PlanName.Contains("Fatura")).ToListAsync();
            return list;
        }
    }
}
