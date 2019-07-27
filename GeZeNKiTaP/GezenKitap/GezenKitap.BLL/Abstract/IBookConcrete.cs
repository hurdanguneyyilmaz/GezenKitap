using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.BLL.Abstract
{
    public interface IBookConcrete
    {
        IEnumerable<Book> GetBooksbyCategoryID(int categoryId, string userId);
        void AddBook(Book book);
        void Update(Book book);
        IEnumerable<Book> MyBooks(string userId);
        Book GetBook(int ID);
        IEnumerable<Book> GetBooks(string userId);
        bool DeleteBook(int item);

    }
}
