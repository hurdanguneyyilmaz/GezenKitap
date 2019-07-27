using GezenKitap.BLL.Repository;
using GezenKitap.BLL.UnitOfWork;
using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.BLL.Concrete
{
    public class BookConcrete
    {
        public IRepository<Book> BookRepository;
        public IUnitOfWork BookUnitOfWork;
        private ApplicationDbContext _dbContext;

        public BookConcrete()
        {
            _dbContext = new ApplicationDbContext();
            BookUnitOfWork = new EFUnitOfWork(_dbContext);
            BookRepository = BookUnitOfWork.GetRepository<Book>();
        }
        //concrete in mevcut kullanıcıyı bilmesi mümkün olmadığı için userId değişkenini ekledik...
        public IEnumerable<Book> GetBooksbyCategoryID(int categoryId,string userId)
        {
            
            return _dbContext.Books.Where(x => x.CategoryID == categoryId
                        && x.UserID != userId
                        && x.IsActive && !x.IsDelete).ToList();//list döndüğü için yukarda int yerine IEnumerable tanımladık!!!
        }

        public void AddBook(Book book)
        {
            BookRepository.Add(book);
            BookUnitOfWork.SaveChanges();
        }

        public void Update(Book book)
        {
            BookRepository.Update(book);
            BookUnitOfWork.SaveChanges();
        }

        public IEnumerable<Book> MyBooks(string userId)
        {
            return _dbContext.Books.Where(x => x.UserID == userId && !x.IsDelete).ToList();
        }

        public Book GetBook(int ID)
        {            
            return BookRepository.GetById(ID);
        }

        public IEnumerable<Book> GetBooks(string userId)
        {
            return _dbContext.Books.Where(x => x.UserID != userId && x.IsDelete == false && x.IsActive == true).OrderBy(r => Guid.NewGuid()).Take(8).ToList();
        }

        public bool DeleteBook(int item)
        {
            BookRepository.Delete(item);

            return BookUnitOfWork.SaveChanges() == 1;//silme işlemi başarılıysa 1 dönüyor.
        }
    }
}
