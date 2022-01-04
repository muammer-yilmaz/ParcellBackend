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
        public double Internet { get; set; }
        public int Minutes { get; set; }
        public int Sms { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
