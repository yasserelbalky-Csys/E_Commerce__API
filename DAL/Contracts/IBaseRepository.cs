using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        IEnumerable<Entity> GetAll();

        Entity GetById(int id);

        void Insert(Entity entity);

        void Update(Entity entity);

        void Delete(int id);
    }
}