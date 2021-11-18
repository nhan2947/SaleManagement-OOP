using System;

namespace R2S.Training.Entities
{
    class Order
    {
        private int _orderId;
        private System.DateTime _orderDate;
        private int _customerId;
        private int _employeeId;
        private double _total;

        public Order(int orderId, DateTime orderDate, int customerId, int employeeId, double total)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            CustomerId = customerId;
            EmployeeId = employeeId;
            Total = total;
        }

        public int OrderId { get => _orderId; set => _orderId = value; }
        public DateTime OrderDate { get => _orderDate; set => _orderDate = value; }
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public int EmployeeId { get => _employeeId; set => _employeeId = value; }
        public double Total { get => _total; set => _total = value; }

        public override string? ToString()
        {
            return String.Format($"{OrderId,7}{OrderDate,30}{CustomerId,15}{EmployeeId,15}{Total,15}");
        }
    }
}