using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models {
    public class Device : BaseMongoModel {

        public string Name { get; set; }
        public string DetailText { get; set; }
        public double Price { get; set; }
        public string ImageDirectory { get; set; }
        public string Type { get; set; }
    }

}
