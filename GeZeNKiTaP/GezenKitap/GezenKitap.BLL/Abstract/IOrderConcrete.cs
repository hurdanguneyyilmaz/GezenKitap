using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.BLL.Abstract
{
    public interface IOrderConcrete
    {
        IEnumerable<Order> GetOrders(string userId, int type);
        int RequestCount(string userId);
        int RequestedCount(string userId);
        void RemoveFromCart(int orderId);
        Order OrderFoD(int ID);
        void AddOrder(Order order);
    }
}
