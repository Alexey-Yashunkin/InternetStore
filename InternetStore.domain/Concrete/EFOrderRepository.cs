using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetStore.Domain.Abstract;
using InternetStore.Domain.Entities;

namespace InternetStore.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Order> Orders
        {
            get { return context.Orders; }
        }
        public void AddOrder(Order order)
        {
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order dbEntry = context.Orders.Find(order.OrderID);
                if (dbEntry != null)
                {
                    dbEntry.Cart = order.Cart;
                    dbEntry.Delivery = order.Delivery;
                    dbEntry.Payment = order.Payment;
                    dbEntry.StartDate = order.StartDate;
                    dbEntry.StopDate = order.StopDate;
                    dbEntry.OrderStatus = order.OrderStatus;
                    dbEntry.PayStatus = order.PayStatus;
                    dbEntry.UserID = order.UserID;
                }
            }
            context.SaveChanges();
        }

        public Order DeleteOrder(int orderID)
        {
            Order dbEntry = context.Orders.Find(orderID);
            if (dbEntry != null) {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void UpdateOrderStatus(int orderID, string orderStatus)
        {
            Order dbEntry = context.Orders.Find(orderID);
            if (dbEntry != null)
            {
                dbEntry.OrderStatus = orderStatus;
            }
            context.SaveChanges();
        }

        public void UpdatePayStatus(int orderID, string payStatus)
        {
            Order dbEntry = context.Orders.Find(orderID);
            if (dbEntry != null)
            {
                dbEntry.PayStatus = payStatus;
            }
            context.SaveChanges();
        }
    }
}
