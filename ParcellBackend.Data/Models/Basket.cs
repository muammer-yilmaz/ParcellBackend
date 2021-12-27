using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models {
    public class Basket : BaseMongoModel {
        public string UserId { get; set; }
        public List<string> BasketDevices { get; set; }

    }
}
