using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class DeviceDetailsServiceRepository : BaseMongoRepository<DeviceDetails> {

        public DeviceDetailsServiceRepository(IDbClient<DeviceDetails> dbClient) : base(dbClient) {

        }
        public override Task Create(DeviceDetails model) {
            return base.Create(model);
        }

        public override Task Delete(string id) {
            return base.Delete(id);
        }

        public override Task<DeviceDetails> Get(string id) {
            return base.Get(id);
        }

        public override Task<List<DeviceDetails>> GetList() {
            return base.GetList();
        }

        public override Task Update(string id, DeviceDetails model) {
            return base.Update(id, model);
        }

        public async Task<DeviceDetails> GetDeviceDetail(string deviceId) {
            var detail = await base.modelMongoCollection.Find(x => x.DeviceId == deviceId).FirstOrDefaultAsync();
            return detail;
        }

        public async Task<Phone> GetPhoneDetail(string deviceId) {
            var detail = await base.modelMongoCollection.Find(x => x.DeviceId == deviceId).FirstOrDefaultAsync();
            return detail.Phone;
        }
        public async Task<Headphone> GetHeadphoneDetail(string deviceId) {
            var detail = await base.modelMongoCollection.Find(x => x.DeviceId == deviceId).FirstOrDefaultAsync();
            return detail.Headphone;
        }
        public async Task<Powerbank> GetPowerbankDetail(string deviceId) {
            var detail = await base.modelMongoCollection.Find(x => x.DeviceId == deviceId).FirstOrDefaultAsync();
            return detail.Powerbank;
        }
    }
}
