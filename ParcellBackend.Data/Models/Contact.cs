using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models
{
    public class Contact : BaseMongoModel
    {
        public string Header { get; set; }
        public string HeaderText { get; set; }
        public string Address { get; set; }
    }
}
