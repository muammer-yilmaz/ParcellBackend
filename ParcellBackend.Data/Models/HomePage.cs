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

        public List<Card> Cards { get; set; }
    }
    public class Card
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CardText { get; set; }
        public string CardImage { get; set; }
    }
}
