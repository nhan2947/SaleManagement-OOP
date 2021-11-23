using R2S.Training.Main;
using System.Data;
using System.Data.SqlClient;

namespace R2S.Training.ADO
{
    internal class ProductDAO : IProductDAO
    {
        private DatabaseCRUD _database;
        public ProductDAO(DatabaseCRUD database)
        {
            _database = database;
        }

        public double GetProductPrice(int productId)
        {
            if(IsProductExist(productId))
            {
                string sqlQuery = String.Format("Select product_price from Product where product_id = @product_id");
                SqlCommand command = new SqlCommand(sqlQuery);

                command.Parameters.AddWithValue("@product_id", productId);
                DataTable dataTable = _database.DataRetrieve(command);
                return Convert.ToDouble(dataTable.Rows[0]["product_price"]);

            }
            else
            {
                Console.WriteLine("Product doesn't exist!");
                return 0;
            }
        }

        public bool IsProductExist(int productId)
        {
            string sqlQuery = String.Format("SELECT * FROM dbo.Product WHERE product_id = @product_id");
            SqlCommand command = new SqlCommand(sqlQuery);

            command.Parameters.AddWithValue("@product_id", productId);
            DataTable dataTable = _database.DataRetrieve(command);
            return dataTable.Rows.Count > 0;
        }
    }
}
