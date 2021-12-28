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
        public Cards Card1 { get; set; }

        public Cards Card2 { get; set; }

        public Cards Card3 { get; set; }

    }
    public class Cards
    {
        public string Card { get; set; }
        public string CardImage { get; set; }
    }
    
}
