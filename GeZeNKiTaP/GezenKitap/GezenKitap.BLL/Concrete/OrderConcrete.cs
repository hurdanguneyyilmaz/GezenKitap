using GezenKitap.BLL.Abstract;
using GezenKitap.BLL.Repository;
using GezenKitap.BLL.UnitOfWork;
using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
using GezenKitap.DATA.EnumsInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.BLL.Concrete
{
    public class OrderConcrete : IOrderConcrete
    {
        public IRepository<Order> OrderRepository;
        public IUnitOfWork OrderUnitOfWork;
        private ApplicationDbContext _dbContext;

        public OrderConcrete()
        {
            _dbContext = new ApplicationDbContext();
            OrderUnitOfWork = new EFUnitOfWork(_dbContext);
            OrderRepository = OrderUnitOfWork.GetRepository<Order>();//?????
        }
        //concrete in mevcut kullanıcıyı bilmesi mümkün olmadığı için userId değişkenini ekledik...
        public IEnumerable<Order> GetOrders(string userId,int type)
        {
            IEnumerable<Order> orders;
            if (type == 1)
                orders = _dbContext.Orders.Where(x => x.ApplicationUser_Id == userId
                && x.State != OrderState.Tamamlandi).ToList();
            else
                orders = _dbContext.Orders.Where(x => x.Book.UserID == userId
                            && x.State != OrderState.Tamamlandi).ToList();

            return orders;
        }

        public int RequestCount(string userId)
        {
            //gri ile yazılan yerler yazılmasada olur!
            return OrderRepository.GetAll(x => x.ApplicationUser_Id == userId
                    && x.State != GezenKitap.DATA.EnumsInterface.OrderState.Tamamlandi).Count();
        }

        public int RequestedCount(string userId)
        {
            return OrderRepository.GetAll(x => x.Book.UserID == userId
                            && x.State != GezenKitap.DATA.EnumsInterface.OrderState.Tamamlandi).Count();
        }

        public void RemoveFromCart(int orderId)
        {
            var order = this.OrderRepository.Get(x => x.OrderID == orderId);
            order.State = OrderState.Iptal;
            this.OrderRepository.Update(order);
            this.OrderUnitOfWork.SaveChanges();
        }

        public bool RequestedOrder(string userId,int requestId)
        {
            return _dbContext.Orders.Any(x => x.BookID == requestId && x.ApplicationUser_Id == userId
                 && x.State >= OrderState.Istek);
        }
        public Order OrderFoD(int ID)
        {
            return _dbContext.Orders.FirstOrDefault(x => x.BookID == ID
                 && x.State >= OrderState.Kargolandi);
        }

        public void AddOrder(Order order)
        {
            OrderRepository.Add(order);
            OrderUnitOfWork.SaveChanges();
        }
    }
}
