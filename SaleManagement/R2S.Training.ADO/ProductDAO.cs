using R2S.Training.Main;
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
            SqlCommand command = new SqlCommand("SELECT dbo.GetProductPrice(@product_id)");
            command.Parameters.AddWithValue("@product_id", productId);
            return Convert.ToDouble(_database.DataCalculator(command));
        }

        public bool IsProductExist(int productId)
        {
            SqlCommand command = new SqlCommand("SELECT dbo.IsProductExist(@product_id)");
            command.Parameters.AddWithValue("@product_id", productId);
            return Convert.ToBoolean(_database.DataCalculator(command));
        }
    }
}
