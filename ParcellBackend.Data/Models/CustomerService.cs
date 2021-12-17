using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models
{
    public class CustomerService : BaseMongoModel
    {
        public string Header { get; set; }
        public string HeaderText { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Question2 { get; set; }
        public string Answer2 { get; set; }
    }
}
