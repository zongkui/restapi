using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GroceryStoreAPI.DataAccess
{
    public class FileRepository : IRepository
    {
        protected List<CustomerModel> _customers;
        private const string _dataFile = "database.json";
        public FileRepository()
        {
            _customers = LoadDataFile(_dataFile);
        }
        public List<CustomerModel> GetAll()
        {
            return _customers;
        }
        public CustomerModel Get(int id)
        {
            return _customers.FirstOrDefault(x => x.Id == id);
        }
        public CustomerModel Save(CustomerModel customer)
        {
            if (customer.Id == 0)
            {
                // Insert new record
                int maxId = _customers.Max(x => x.Id);
                customer.Id = maxId + 1;
                _customers.Add(customer);
            }
            else
            {
                // Update existing record
                var found = _customers.FirstOrDefault(x => x.Id == customer.Id);
                found.Name = customer.Name;
            }
            return customer;
        }
        public void Delete(int id)
        {
            var found = _customers.FirstOrDefault(x => x.Id == id);
            _customers.Remove(found);
        }

        protected List<CustomerModel> LoadDataFile(string filename)
        {
            var rawData = File.ReadAllText(_dataFile);
            var tokens = JToken.Parse(rawData);
            var data = tokens?.SelectToken("customers");
            return JsonConvert.DeserializeObject<List<CustomerModel>>(data?.ToString());
        }
    }
}
