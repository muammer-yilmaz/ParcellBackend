using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models {
    public class Order : BaseMongoModel{

        public string UserId { get; set; }
        public List<string> DeviceIds { get; set; }
        public string OrderAddress { get; set; }
        public double TotalPrice { get; set; }
    }
}
