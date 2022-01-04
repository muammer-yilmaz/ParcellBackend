using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models {
    public class Invoice : BaseMongoModel {

        public string UserId { get; set; }
        public string PlanId { get; set; }
        public DateTime ContractDate { get; set; }
        public string ContractTime { get; set; }

    }
}
