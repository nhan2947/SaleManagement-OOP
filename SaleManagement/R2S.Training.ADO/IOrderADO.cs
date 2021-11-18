using System.Collections.Generic;
using R2S.Training.Entities;

namespace R2S.Training.ADO
{
    interface IOrderADO
    {
        List<Order> GetAllOrdersByCustomerId(int customerId);
        double ComputeOrderTotal(int orderId);
        bool AddOrder(Order order);
        bool UpdateOrderTotal(int orderId);
    }
}