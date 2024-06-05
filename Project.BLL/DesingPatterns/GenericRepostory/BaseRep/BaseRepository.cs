using Project.BLL.DesingPatterns.GenericRepostory.IntRep;
using Project.BLL.DesingPatterns.SingletonPattern;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesingPatterns.GenericRepostory.BaseRep
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
         return  _db.Set<T>().Any(exp);
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
            throw new NotImplementedException();
        }

        public void DestroyRange(List<T> list)
        {
            throw new NotImplementedException();
        }

        public T Find(int id)
        {
        
         return   _db.Set<T>().Find(id);
        
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
          return  _db.Set<T>().FirstOrDefault(exp);
        }

        public List<T> GetActives()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<T> GetFirstDatas(int number)
        {
            throw new NotImplementedException();
        }

        public List<T> GetLastData(int number)
        {
            throw new NotImplementedException();
        }

        public List<T> GetPassives()
        {
            throw new NotImplementedException();
        }

        public List<T> GetUpdateds()
        {
           return Where(x => x.Status == ENTITIES.Enums.DateStatus.Modified);

        }

        public object Select(Expression<Func<T, bool>> exp)
        {
          return  _db.Set<T>().Select(exp);
        }

       

        public void Update(T item)
        {
           item.ModifiedDate= DateTime.Now;
            item.Status = ENTITIES.Enums.DateStatus.Modified;
            Save();
        }

        public void UpdateRange(List<T> list)
        {
            foreach (T item in list)
            {
                Update(item);
            }
        }

      

        public IQueryable<X> SelectViaClass<X>(Expression<Func<T, X>> exp)
        {
            throw new NotImplementedException();
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Where(exp).ToList();
        }
    }
}
         

      

       
        

        
        
 
   
