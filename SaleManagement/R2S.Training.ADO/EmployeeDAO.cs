using R2S.Training.Main;
using System.Data;
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
            string sqlQuery = String.Format("SELECT * FROM dbo.Employee WHERE employee_id = @employee_id");
            SqlCommand command = new SqlCommand(sqlQuery);
            command.Parameters.AddWithValue("@employee_id", employeeId);

            DataTable dataTable = _database.DataRetrieve(command);
            return dataTable.Rows.Count > 0;
        }
    }
}
