using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.ADO
{

    public interface IEmployeeDAO
    {
        bool IsEmployeeExist(int employeeId);
    }
}
