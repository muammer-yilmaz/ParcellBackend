using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models {

    public class ParcellDatabaseSettings {

        public string DbConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string BaseCollectionName { get; set; }

        public string CollectionNameUser  { get; set; }
    }
}
