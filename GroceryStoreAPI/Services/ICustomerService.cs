using GroceryStoreAPI.DataAccess;
using GroceryStoreAPI.Models;
using System.Collections.Generic;

namespace GroceryStoreAPI.Services
{
    public interface ICustomerService
    {
        List<CustomerReadModel> GetAll();
        CustomerReadModel Get(int id);
        int Create(CustomerUpdateModel customer);
        void Update(int id, CustomerUpdateModel customer);
        void Delete(int id);
        string ValidateUpdate(CustomerUpdateModel customer);
        bool Exists(int id);
        CustomerModel ToModel(CustomerUpdateModel customer);
        CustomerReadModel ToViewModel(CustomerModel customer);
    }
}
