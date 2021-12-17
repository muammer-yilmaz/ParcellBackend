using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models
{
    public class Promotion : BaseMongoModel
    {
        public string PromoName { get; set; }
        public string PromoDesc { get; set; }
        public string PromoCode { get; set; }
        public string Image { get; set; }
    }
}
