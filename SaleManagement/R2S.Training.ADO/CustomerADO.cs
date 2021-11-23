using R2S.Training.Entities;
using R2S.Training.Main;
using System.Data.SqlClient;
using System.Data;

namespace R2S.Training.ADO
{
    class CustomerADO : ICustomerADO
    {
        private DatabaseCRUD _database;
        public CustomerADO(DatabaseCRUD database)
        {
            _database = database;
        }
        public bool AddCustomer(Customer customer)
        {
            SqlCommand command = new SqlCommand("dbo.AddCustomer");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@customer_name", customer.CustomerName);

            try
            {
                _database.DataModifier(command);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            SqlCommand command = new SqlCommand("dbo.DeleteCustomer");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@customer_id", customerId);
            return _database.DataModifier(command) > 0;
        }

        public List<Customer> GetAllCustomers()
        {
            string sqlQuery = "Select * from Customer";
            SqlCommand command = new SqlCommand(sqlQuery);
            DataTable data= _database.DataRetrieve(command);
            return GetCustomerList(data);
        }

        public bool IsCustomerExist(int customerId)
        {
            string sqlQuery = String.Format("SELECT * FROM dbo.Customer WHERE customer_id = @customer_id");
            SqlCommand command = new SqlCommand(sqlQuery);
            command.Parameters.AddWithValue("@customer_id", customerId);
            
            DataTable dataTable = _database.DataRetrieve(command);
            return dataTable.Rows.Count > 0;
        }

        public bool UpdateCustomer(Customer customer)
        {
            SqlCommand command = new SqlCommand("dbo.UpdateCustomer");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@customer_id", customer.CustomerId);
            command.Parameters.AddWithValue("@customer_name", customer.CustomerName);
            return _database.DataModifier(command) > 0;
        }

        private List<Customer> GetCustomerList(DataTable data)
        {
            if (data == null)
            {
                return null;
            }
            List<Customer> customerList = new List<Customer>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int id = Convert.ToInt32(data.Rows[i]["customer_id"]);
                string name = data.Rows[i]["customer_name"].ToString();
                Customer customer = new Customer(id, name);
                customerList.Add(customer);
            }
            return customerList;
        }
    }
}