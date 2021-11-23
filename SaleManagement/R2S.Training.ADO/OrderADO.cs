using System.Collections.Generic;
using R2S.Training.Entities;
using R2S.Training.Main;
using System.Data.SqlClient;
using System.Data;

namespace R2S.Training.ADO
{
    class OrderADO : IOrderADO
    {
        private DatabaseCRUD _database;
        public OrderADO(DatabaseCRUD database)
        {
            _database = database;
        }


        public bool AddOrder(Order order)
        {
            SqlCommand command = new SqlCommand("dbo.AddOrder");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@order_date", order.OrderDate);
            command.Parameters.AddWithValue("@customer_id", order.CustomerId);
            command.Parameters.AddWithValue("@employee_id", order.EmployeeId);
            command.Parameters.AddWithValue("@total", order.Total);

            try
            {
                _database.DataModifier(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool DeleteOrder(int orderId)
        {
            SqlCommand command = new SqlCommand("dbo.DeleteOrder");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@order_id", orderId);
            return _database.DataModifier(command) > 0;
        }

        public double ComputeOrderTotal(int orderId)
        {
            SqlCommand command = new SqlCommand("SELECT dbo.ComputeOrderTotal(@order_id)");
            command.Parameters.AddWithValue("@order_id", orderId);
            return Convert.ToDouble(_database.DataCalculator(command));
        }

        public List<Order> GetAllOrdersByCustomerId(int customerId)
        {
            string sqlQuery = String.Format("select * from Orders where customer_id=@customer_id");
            SqlCommand command = new SqlCommand(sqlQuery);
            command.Parameters.AddWithValue("@customer_id", customerId);
            DataTable data = _database.DataRetrieve(command);
            return GetOrderList(data);
        }

        public bool IsOrderExist(int orderId)
        {
            SqlCommand command = new SqlCommand("SELECT dbo.IsOrderExist(@order_id)");
            command.Parameters.AddWithValue("@order_id", orderId);
            return Convert.ToBoolean(_database.DataCalculator(command));
        }

        public bool UpdateOrderTotal(int orderId)
        {
            double orderTotal = ComputeOrderTotal(orderId);
            string updateQuery = String.Format("Update Orders SET total=@total Where order_id = @order_id;");
            SqlCommand command = new SqlCommand(updateQuery);
            command.Parameters.AddWithValue("@total", orderTotal);
            command.Parameters.AddWithValue("@order_id", orderId);

            return _database.DataModifier(command) > 0;
        }

        private List<Order> GetOrderList(DataTable data)
        {
            if(data == null)
            {
                return null;
            }
            List<Order> orderList = new List<Order>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int orderId = Convert.ToInt32(data.Rows[i]["order_id"]);
                DateTime dateTime = Convert.ToDateTime(data.Rows[i]["order_date"]);
                int customerId = Convert.ToInt32(data.Rows[i]["customer_id"]);
                int employeeId = Convert.ToInt32(data.Rows[i]["employee_id"]);
                double total = Convert.ToDouble(data.Rows[i]["total"]);
                Order order = new Order(orderId, dateTime, customerId, employeeId, total);
                orderList.Add(order);
            }
            return orderList;
        }

    }
}