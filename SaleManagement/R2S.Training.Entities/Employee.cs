namespace R2S.Training.Entities
{
    class Employee
    {
        private int _employeeId;
        private string _employeeName;
        private double _employeeSalary;
        private int _spvrId;

        public Employee(int employeeId, string employeeName, double employeeSalary, int spvrId)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeSalary = employeeSalary;
            SpvrId = spvrId;
        }

        public int EmployeeId { get => _employeeId; set => _employeeId = value; }
        public string EmployeeName { get => _employeeName; set => _employeeName = value; }
        public double EmployeeSalary { get => _employeeSalary; set => _employeeSalary = value; }
        public int SpvrId { get => _spvrId; set => _spvrId = value; }
    }
}