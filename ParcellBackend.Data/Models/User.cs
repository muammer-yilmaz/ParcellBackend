using System;

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
    }
}
