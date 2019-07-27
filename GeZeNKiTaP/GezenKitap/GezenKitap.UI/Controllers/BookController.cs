using GezenKitap.BLL.Repository;
using GezenKitap.BLL.UnitOfWork;
using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
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
    public class BookController : Controller
    {
        BookConcrete bookConcrete;
        CategoryConcrete categoryConcrete;
        AuthorConcrete authorConcrete;
        StatusConcrete statusConcrete;
        ReviewConcrete reviewConcrete;
        //ApplicationDbContext db;
        //EFRepository<Book> repBook;
        //EFUnitOfWork uow;

        public BookController()
        {
            bookConcrete = new BookConcrete();
            categoryConcrete = new CategoryConcrete();
            authorConcrete = new AuthorConcrete();
            statusConcrete = new StatusConcrete();
            reviewConcrete = new ReviewConcrete();
            //db = new ApplicationDbContext();
            //repBook = new EFRepository<Book>(db);
            //uow = new EFUnitOfWork(db);
        }

        public ActionResult Book(int id)
        {
            var UserID = User.Identity.GetUserId();
            //return View(db.Books.Where(x => x.CategoryID == id 
            //            && x.UserID != UserID
            //            && x.IsActive && !x.IsDelete).ToList());
            var model = bookConcrete.GetBooksbyCategoryID(id, UserID);
            return View(model);//burda kurduğumuz modelle view tarafındaki Model ile burayı geri alıyoruz...
        }

        [Authorize]
        public ActionResult AddBook(int? id)
        {
            ViewData["CategoryID"] = new SelectList(
                //db.Categories.ToList()
                categoryConcrete.GetCategoryList()
                .Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                //db.Authors.ToList()
                authorConcrete.GetAuthorList()
                .Select(x => new {
                    x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(
                //db.Statuses.ToList()
                statusConcrete.GetStatusList()
                .Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");

            var model = new Book();
            if (id.HasValue) model.CategoryID = id.Value;

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddBook(Book model)
        {
            ViewData["CategoryID"] = new SelectList(
                //db.Categories.ToList()
                categoryConcrete.GetCategoryList()
                .Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                //db.Authors.ToList()
                authorConcrete.GetAuthorList()
                .Select(x => new {
                    x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(
                //db.Statuses.ToList()
                statusConcrete.GetStatusList()
                .Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");

            model.UserID = User.Identity.GetUserId();
            model.IsDelete = false;
            model.IsActive = true;

            if (Request.Files.Count > 0)
            {
                try
                {
                    var file = Request.Files[0];
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/img/Books"), pic);
                    // file is uploaded
                    file.SaveAs(path);

                    model.ImageUrl = "~/img/Books/" + pic;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", "Resim yüklenemiyor!" + ex.Message);

                    return View(model);
                }
            }

            if (false && !ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Bir hata oluştu!");

                return View(model);
            }
            else
            {
                try
                {
                    //repBook.Add(model);
                    //uow.SaveChanges();
                    bookConcrete.AddBook(model);

                    return RedirectToAction("MyBooks","Book");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex);

                    return View(model);
                }
            }
        }

        [Authorize]
        //int dolu geleceği için  int in yanındaki ? işaretini kaldırdık...
        public ActionResult UpdateBook(int id)
        {
            ViewData["CategoryID"] = new SelectList(
                //db.Categories.ToList()
                categoryConcrete.GetCategoryList()
                .Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                //db.Authors.ToList()
                authorConcrete.GetAuthorList()
                .Select(x => new {
                    x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(
                //db.Statuses.ToList()
                statusConcrete.GetStatusList()
                .Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");

            var model = bookConcrete.BookRepository.GetById(id);
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public ActionResult UpdateBook(Book model)
        {
            ViewData["CategoryID"] = new SelectList(
                //db.Categories.ToList()
                categoryConcrete.GetCategoryList()
                .Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                //db.Authors.ToList()
                authorConcrete.GetAuthorList()
                .Select(x => new {
                    x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(
                //db.Statuses.ToList()
                statusConcrete.GetStatusList()
                .Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");
            
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                try
                {
                    var file = Request.Files[0];
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/img/Books"), pic);
                    // file is uploaded
                    file.SaveAs(path);
                    model.ImageUrl = "~/img/Books/" + pic;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", "Resim yüklenemiyor!" + ex.Message);

                    return View(model);
                }
            }

            if (false && !ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Bir hata oluştu!");

                return View(model);
            }
            else
            {
                try
                {
                    //repBook.Update(model);
                    //uow.SaveChanges();
                    bookConcrete.Update(model);

                    return RedirectToAction("MyBooks");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex);

                    return View(model);
                }
            }
        }


        public ActionResult MyBooks()
        {
            var UserID = User.Identity.GetUserId();
            //return View(db.Books.Where(x => x.UserID == UserID && !x.IsDelete).ToList());
            var model = bookConcrete.MyBooks(UserID);
            return View(model);
        }

        public ActionResult BookDetail(int id)
        {
            ViewData["Reviews"] = reviewConcrete.GetReviewsbyBookID(id); //db.Reviews.Where(x => x.BookID == id && x.IsDeleted == false).ToList();
                
            return View(bookConcrete.GetBook(id));
        }
                
        [HttpPost]
        public ActionResult AddReview(int id, FormCollection frm)
        {
            Review review = new Review()
            {
                Comment = frm["review"],
                ApplicationUser_Id = User.Identity.GetUserId(),
                DateTime = DateTime.Now,
                BookID = id,
                Name = frm["name"] == "" ? "Misafir Kullanıcı" : frm["name"],
                Rate = int.Parse(frm["rate"])
            };

            //db.Reviews.Add(review);
            //db.SaveChanges();
            reviewConcrete.AddReview(review);

            return RedirectToAction("BookDetail", new { id = id });
        }
    }
}
