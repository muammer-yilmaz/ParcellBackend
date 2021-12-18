using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models
{
    public class Plan : BaseMongoModel
    {
        public string PlanName { get; set; }
        public string Internet { get; set; }
        public string Minutes { get; set; }
        public string Sms { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
