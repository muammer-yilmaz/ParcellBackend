using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models
{
    public class Faq : BaseMongoModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
