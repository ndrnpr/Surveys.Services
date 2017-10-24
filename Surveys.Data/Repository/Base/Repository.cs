using System;
using System.Collections.Generic;
using System.Text;
using Surveys.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Surveys.Data.Repository.Base
{
    public abstract class Repository<T> : IDisposable, IRepository<T> where T : SurveyPrincipalEntity, new()
    {
        protected readonly SurveysContext _db;
        protected DbSet<T> Table;
        public SurveysContext Context => _db;

        protected Repository()
        {
            _db = new SurveysContext();
            Table = _db.Set<T>();
        }

        protected Repository(DbContextOptions<SurveysContext> options)
        {
            _db = new SurveysContext(options);
            Table = _db.Set<T>();
        }
        
        public bool HasChanges => _db.ChangeTracker.HasChanges();

        public int Count => Table.Count();

        public T GetFirst() => Table.FirstOrDefault();

        public T Find(int? id) => Table.Find(id);

        public virtual IEnumerable<T> GetAll() => Table;

        internal IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take)
            => query.Skip(skip).Take(take);
        public virtual IEnumerable<T> GetRange(int skip, int take)
            => GetRange(Table, skip, take);

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }
        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }
        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        internal T GetEntryFromChangeTracker(int? id)
        {
            return _db.ChangeTracker.Entries<T>()
                .Select((EntityEntry e) => (T)e.Entity)
                    .FirstOrDefault(x => x.ID == id);
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            var entry = GetEntryFromChangeTracker(id);
            if (entry != null)
            {
                if (entry.TimeStamp.SequenceEqual(timeStamp))
                {
                    return Delete(entry, persist);
                }
                throw new Exception("Unable to delete due to concurrency violation.");
            }
            _db.Entry(new T { ID = id, TimeStamp = timeStamp }).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
            }
            _db.Dispose();
            _disposed = true;
        }
    }
}
