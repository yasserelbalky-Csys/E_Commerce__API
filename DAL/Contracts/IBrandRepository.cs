using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IBrandRepository:IBaseRepository<Brands>
    {
       // public IEnumerable<Brands> getBrandsWithItsProducts(int brandid);




    }
}
