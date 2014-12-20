using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetStore.Domain.Entities;

namespace InternetStore.Domain.Abstract
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void AddOrder(Order order);
        Order DeleteOrder(int orderID);
        void UpdatePayStatus(int orderID, string PayStatus);
        void UpdateOrderStatus(int orderID, string OrderSatatus);
    }
}
