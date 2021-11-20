using System;
using System.Collections;

namespace R2S.Training.Main
{
    class SaleManagement
    {
        static void Main()
        {
            string connectionString = @"Data Source=DESKTOP-7HGTNH5\THANHNHAN;Initial Catalog=SMS;Integrated Security=True";
            Manager saleManagement = new Manager(connectionString);
            saleManagement.Manage();
        }
    }
}
