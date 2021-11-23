using R2S.Training.Main;
using System.Data.SqlClient;
namespace R2S.Training.ADO
{
    internal class EmployeeDAO : IEmployeeDAO
    {
        private DatabaseCRUD _database;
        public EmployeeDAO(DatabaseCRUD database)
        {
            _database = database;
        }
        public bool IsEmployeeExist(int employeeId)
        {
            SqlCommand command = new SqlCommand("SELECT dbo.IsEmployeeExist(@employee_id)");
            command.Parameters.AddWithValue("@employee_id", employeeId);
            return Convert.ToBoolean(_database.DataCalculator(command));
        }
    }
}
