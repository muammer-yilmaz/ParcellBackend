using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services
{
    public class FaqServiceRepository : BaseMongoRepository<Faq>
    {
        public FaqServiceRepository(IDbClient<Faq> dbClient) : base(dbClient)
        {

        }

        public override Task Create(Faq model)
        {
            return base.Create(model);
        }

        public override Task Delete(string id)
        {
            return base.Delete(id);
        }

        public override Task<Faq> Get(string id)
        {
            return base.Get(id);
        }

        public override Task<List<Faq>> GetList()
        {
            return base.GetList();
        }

        public override Task Update(string id, Faq model)
        {
            return base.Update(id, model);
        }
    }
}
