using R2S.Training.Entities;
using R2S.Training.Main;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace R2S.Training.ADO
{
    internal class LineItemADO : ILineItemADO
    {
        private DatabaseCRUD _database;
        public LineItemADO(DatabaseCRUD database)
        {
            _database = database;
        }

        public bool AddLineItem(LineItem item)
        {
            string sqlQuery = String.Format("Insert into LineItem (order_id,product_id,quantity, price) Values (@order_id,@product_id ,@quantity, @price) ;");

            SqlCommand command = new SqlCommand(sqlQuery);

            command.Parameters.AddWithValue("@order_id", item.OrderId);
            command.Parameters.AddWithValue("@product_id", item.ProductId);
            command.Parameters.AddWithValue("@quantity", item.Quantity);
            command.Parameters.AddWithValue("@price", item.Price);

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

        public List<LineItem> GetAllItemsByOrderId(int orderId)
        {
            string sqlQuery = String.Format("select * from LineItem where order_id=@order_id");
            SqlCommand command = new SqlCommand(sqlQuery);
            command.Parameters.AddWithValue("@order_id", orderId);
            DataTable data = _database.DataRetrieve(command);
            return GetItemList(data);
        }

        private List<LineItem> GetItemList(DataTable data)
        {
            if (data == null)
            {
                return null;
            }
            List<LineItem> itemList = new List<LineItem>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int orderId = Convert.ToInt32(data.Rows[i]["order_id"]);
                int productId = Convert.ToInt32(data.Rows[i]["product_id"]);
                int quantity = Convert.ToInt32(data.Rows[i]["quantity"]);
                double price = Convert.ToDouble(data.Rows[i]["price"]);
                LineItem item = new LineItem(orderId, productId, quantity, price);
                itemList.Add(item);
            }
            return itemList;
        }
    }
}
