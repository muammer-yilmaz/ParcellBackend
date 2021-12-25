using MongoDB.Driver;
using ParcellBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcellBackend.Data.Services {
    public class DeviceServiceRepository : BaseMongoRepository<Device> {

        public DeviceServiceRepository(IDbClient<Device> dbClient) : base(dbClient) {

        }

        public override Task Create(Device model) {
            return base.Create(model);
        }

        public override Task Delete(string id) {
            return base.Delete(id);
        }

        public override Task<Device> Get(string id) {
            return base.Get(id);
        }

        public override Task<List<Device>> GetList() {
            return base.GetList();
        }

        public override Task Update(string id, Device model) {
            return base.Update(id, model);
        }

        public async Task<List<Device>> GetDeviceList(List<string> deviceIds) {

            List<Device> deviceList = new List<Device>();

            var AllList = await GetList();

            foreach(var item in deviceIds) {
                deviceList.Add(AllList.Find(x => x.Id == item));
            }

            return deviceList;

        }

    }
}
