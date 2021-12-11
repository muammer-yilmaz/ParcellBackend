﻿using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class UserServiceRepository : BaseMongoRepository<User> {

        public UserServiceRepository(IDbClient<User> dbClient) : base(dbClient) {

        }


        public override Task<List<User>> GetList() {
            return base.GetList();
        }

        public override Task<User> Get(string id) {
            return base.Get(id);
        }

        public override Task Create(User model) {
            return base.Create(model);
        }

        public override Task Update(string id, User model) {
            return base.Update(id, model);
        }

        public override Task Delete(string id) {
            return base.Delete(id);
        }



    }
}
