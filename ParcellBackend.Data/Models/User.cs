using System;
using System.ComponentModel.DataAnnotations;

namespace ParcellBackend.Data.Models {
    public class User : BaseMongoModel {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public double Balance { get; set; }
        public string PlanId { get; set; }
        public RemaingUsage? RemaingUsage { get; set; }
    }

    public class RemaingUsage {
        public double Internet { get; set; }
        public int Minutes { get; set; }
        public int Sms { get; set; }
    }
}
