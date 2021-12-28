using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services
{
    public class HomePageServiceRepository : BaseMongoRepository<HomePage>
    {
        public HomePageServiceRepository(IDbClient<HomePage> dbClient) : base(dbClient)
        {

        }

        public override Task Create(HomePage model)
        {
            return base.Create(model);
        }

        public override Task Delete(string id)
        {
            return base.Delete(id);
        }

        public override Task<HomePage> Get(string id)
        {
            return base.Get(id);
        }

        public override Task<List<HomePage>> GetList()
        {
            return base.GetList();
        }

        public override Task Update(string id, HomePage model)
        {
            return base.Update(id, model);
        }

    }
}
