using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services
{
    public class PromotionServiceRepository : BaseMongoRepository<Promotion>
    {
        public PromotionServiceRepository(IDbClient<Promotion> dbClient) : base(dbClient)
        {

        }

        public override Task Create(Promotion model)
        {
            return base.Create(model);
        }

        public override Task Delete(string id)
        {
            return base.Delete(id);
        }

        public override Task<Promotion> Get(string id)
        {
            return base.Get(id);
        }

        public override Task<List<Promotion>> GetList()
        {
            return base.GetList();
        }

        public override Task Update(string id, Promotion model)
        {
            return base.Update(id, model);
        }
    }
}
