using System.Collections.Generic;
using R2S.Training.Entities;

namespace R2S.Training.ADO
{
    interface ICustomerADO
    {
        List<Customer> GetAllCustomers();
        bool AddCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(Customer customer);
        bool IsCustomerExist(int customerId);
    }
}