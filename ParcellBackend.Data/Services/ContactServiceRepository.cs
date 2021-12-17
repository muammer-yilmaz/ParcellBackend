using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services
{
    public class ContactServiceRepository : BaseMongoRepository<Contact>
    {
        public ContactServiceRepository(IDbClient<Contact> dbClient) : base(dbClient)
        {

        }

        public override Task Create(Contact model)
        {
            return base.Create(model);
        }

        public override Task Delete(string id)
        {
            return base.Delete(id);
        }

        public override Task<Contact> Get(string id)
        {
            return base.Get(id);
        }

        public override Task<List<Contact>> GetList()
        {
            return base.GetList();
        }

        public override Task Update(string id, Contact model)
        {
            return base.Update(id, model);
        }
    }
}
