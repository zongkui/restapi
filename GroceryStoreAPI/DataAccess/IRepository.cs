using System.Collections.Generic;

namespace GroceryStoreAPI.DataAccess
{
    public interface IRepository
    {
        List<CustomerModel> GetAll();
        CustomerModel Get(int id);
        CustomerModel Save(CustomerModel customer);
        void Delete(int id);
    }
}
