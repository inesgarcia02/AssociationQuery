using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Domain.IRepository;

namespace DataModel.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        public GenericRepository(DbContext context)
        {   
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T? GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }

    public class GenericRepository : IGenericRepository
    {
        protected readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        async Task IGenericRepository.AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }

        async Task IGenericRepository.AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        async Task<IEnumerable<T>> IGenericRepository.FindAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        async Task<IEnumerable<T>> IGenericRepository.GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        async Task<T?> IGenericRepository.GetByIdAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        async Task<T?> IGenericRepository.GetByIdAsync<T>(Guid id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        void IGenericRepository.Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        void IGenericRepository.RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
