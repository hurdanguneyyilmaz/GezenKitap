using GezenKitap.BLL.Abstract;
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
    public class AuthorConcrete : IAuthorConcrete
    {
        public IRepository<Author> authorRepository;
        public IUnitOfWork authorUnitOfWork;
        private ApplicationDbContext _dbContext;

        public AuthorConcrete()
        {
            _dbContext = new ApplicationDbContext();
            authorUnitOfWork = new EFUnitOfWork(_dbContext);
            authorRepository = authorUnitOfWork.GetRepository<Author>();
        }

        public IEnumerable<Author> GetAuthorList()
        {
            return authorRepository.GetAll().ToList();
        }

        public int AddAuthor(Author author)
        {
            authorRepository.Add(author);
            authorUnitOfWork.SaveChanges();

            return author.AuthorID;
        }
    }
}
