using System.Data;
using System.Data.SqlClient;

namespace R2S.Training.Main
{
    class DatabaseCRUD
    {
        private SqlConnection _sqlConnection;
        public DatabaseCRUD(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                try
                {
                    Console.WriteLine("---------------Connecting...");
                    _sqlConnection.Open();
                    Console.WriteLine("---------------Connected!");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("---------------Error when connecting to database: " + e.Message);
                    return false;
                }
            }
            return false;
        }

        public void CloseConnection()
        {
            if (_sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection?.Close();
            }
        }

        public object DataCalculator(SqlCommand command)
        {
            if (OpenConnection())
            {
                try
                {
                    command.Connection = _sqlConnection;
                    return command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return null;
                }
                finally
                {
                    CloseConnection();
                }
            }
            else
            {
                return null;
            }
        }

        public DataTable DataRetrieve(SqlCommand command)
        {
            if (OpenConnection())
            {
                DataTable dataTable = new DataTable();
                try
                {
                    command.Connection = _sqlConnection;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return null;
                }
                finally
                {
                    CloseConnection();
                }
            }
            else
            {
                return null;
            }
        }

        public int DataModifier(SqlCommand command)
        {
            if (OpenConnection())
            {
                try
                {
                    command.Connection = _sqlConnection;
                    return command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return 0;
                }
                finally
                {
                    CloseConnection();
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
