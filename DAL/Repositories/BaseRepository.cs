using DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly DbSet<Entity> _entitySet;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entitySet = _appDbContext.Set<Entity>();
        }

        public virtual IEnumerable<Entity> GetAll()
        {
            return _entitySet.AsEnumerable();
        }

        public virtual Entity GetById(int id)
        {
            return _entitySet.Find(id)!;
        }

        public void Insert(Entity entity)
        {
            _entitySet.Add(entity);
        }

        public void Update(Entity entity)
        {
            _entitySet.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _entitySet.Find(id);

            if (entity != null) {
                _entitySet.Remove(entity);
            }
        }
    }
}