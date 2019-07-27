using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.BLL.Abstract
{
    public interface ICategoryConcrete
    {
        IEnumerable<Category> GetCategoryList();
    }
}
