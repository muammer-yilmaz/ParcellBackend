using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models
{
    public class HomePage : BaseMongoModel
    {
        public string HeaderText { get; set; }
        public string Logo { get; set; }

        public string Card1 { get; set; }
        public string CardImage1 { get; set; }

        public string Card2 { get; set; }
        public string CardImage2 { get; set; }

        public string Card3 { get; set; }
        public string CardImage3 { get; set; }
    }
    
}
