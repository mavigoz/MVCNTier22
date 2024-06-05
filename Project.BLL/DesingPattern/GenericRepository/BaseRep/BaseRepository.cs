using Project.BLL.DesingPattern.GenericRepository.IntRep;
using Project.BLL.DesingPattern.SingletonPatterns;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesingPattern.GenericRepository.BaseRep
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        MyContext _db;
        public BaseRepository()
        {
            _db = DBTool.DbIstance;

        }
        void Save()
        {
            _db.SaveChanges();
        }
        public void Add(T item)
        {
           _db.Set<T>().Add(item);
            Save();
        }

        public void AddRange(List<T> list)
        {
            foreach (T item in list)
            {
                Add(item);

            }
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
           return _db.Set<T>().Any(exp);
        }

        public void Delete(T item)
        {
           item.DeletedDate = DateTime.Now;
            item.Status = ENTITIES.Enums.DateStatus.Deleted;
            Save();
        }

        public void DeleteRange(List<T> list)
        {
            foreach (T item in list)
            {
                Delete(item);
            }
        }

        public void Destroy(T item)
        {
           _db.Set<T>().Remove(item);
            Save();
        }

        public void DestroyRange(List<T> list)
        {
            _db.Set<T>().RemoveRange(list);
            Save();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
          return   _db.Set<T>().FirstOrDefault(exp);
        }

        public List<T> GetActives()
        {
            return Where(x => x.Status != ENTITIES.Enums.DateStatus.Deleted);
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public List<T> GetPassives()
        {
           return  Where(x => x.Status == ENTITIES.Enums.DateStatus.Deleted);
        }

        public List<T> GetUpdateds()
        {
            return Where(x => x.Status == ENTITIES.Enums.DateStatus.Modified);
        }

        public object Select(Expression<Func<T, bool>> exp)
        {
          return _db.Set<T>().Select(exp);
        }

      

        public void Update(T item)
        {
            item.ModifiedDate = DateTime.Now;
            item.Status=ENTITIES.Enums.DateStatus.Modified;
            T tobeUpdated = Find(item.ID);
            _db.Entry(tobeUpdated).CurrentValues.SetValues(item);
            Save();
        }

        public void UpdateRange(List<T> list)
        {
            foreach (T item in list)
            {
                Update(item);
            }
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Where(exp).ToList() ;
        }

        public IQueryable<X> SelectViaClass<X>(Expression<Func<T, X>> exp)
        {
            return _db.Set<T>().Select(exp) ;
        }

        public T Find(int id)
        {
           return _db.Set<T>().Find(id);
        }

        public List<T> FindLastData(int number)
        {
           return _db.Set<T>().OrderByDescending(x=>x.CreatedDate).Take(number).ToList();
        }

        public List<T> FindFirstData(int number)
        {
            return _db.Set<T>().OrderBy(x => x.CreatedDate).Take(number).ToList();
        }
    }
}
