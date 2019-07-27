using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
using GezenKitap.DATA.EnumsInterface;
using GezenKitap.UI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GezenKitap.BLL.Concrete;

namespace GezenKitap.UI.Controllers
{
    public class ShoppingController : Controller
    {
        
        OrderConcrete orderconcrete;
        

        public ShoppingController()
        {
            
            orderconcrete = new OrderConcrete();
            
        }
        

        

        public ActionResult FinishShopping()
        {
            return View();
        }


        

        public ActionResult Cart(int type)//burada önceden int id yazıyordu ama ben onu Order tablosundaki id zannediyordum. meğersem burdaki id nin mantığı  istek ve talep sayfalarını göstermek içinmiş.
        {
            var userid = User.Identity.GetUserId();

            //IEnumerable<Order> orders;
            //if (id == 1)
            //    orders = db.Orders.Where(x => x.ApplicationUser_Id == userid
            //    && x.State != OrderState.Tamamlandi ).ToList();
            //else 
            //    orders = db.Orders.Where(x => x.Book.UserID == userid
            //                && x.State != OrderState.Tamamlandi).ToList();
            

            return View(orderconcrete.GetOrders(userid, type));//yukarda yorum satırına aldığımız yeri orderconcerete içerisinde tanımladık.
        }

        public ActionResult RemoveFromCart(int id)
        {
            //var order = orderconcrete.OrderRepository.Get(x => x.OrderID == id);
            //order.State = OrderState.Iptal;
            //orderconcrete.OrderRepository.Update(order);
            //orderconcrete.OrderUnitOfWork.SaveChanges();
            orderconcrete.RemoveFromCart(id);

            return Redirect(Request.UrlReferrer.ToString());
        }


        

    }
}