using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Models {
    public class DeviceDetails : BaseMongoModel {

        public string DeviceId { get; set; }
#nullable enable
        public Phone? Phone { get; set; }
        public Headphone? Headphone { get; set; }
        public Powerbank? Powerbank { get; set; }
#nullable disable
    }

    public class Phone {
        public string ScreenSize { get; set; }
        public string Proccessor { get; set; }
        public string Ram { get; set; }
        public string Camera { get; set; }
        public string Weight { get; set; }
        public string Memory { get; set; }
        public string Battery { get; set; }

    }

    public class Headphone {
        public string Weight { get; set; }
        public string Color { get; set; }
        public string Microphone { get; set; }
        public string Bluetooth { get; set; }
    }

    public class Powerbank {
        public string Weight { get; set; }
        public string Capacity { get; set; }
        public string FastCharge { get; set; }
        public string Output { get; set; }
    }

}
