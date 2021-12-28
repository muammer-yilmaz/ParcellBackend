using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models {
    public class Order : BaseMongoModel {

        public string UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<string> DeviceIds { get; set; }
        public string OrderAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
