using AuctionApp.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AuctionDbContext _dbContext;
        private DbSet<T> entity => _dbContext.Set<T>();

        public GenericRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity)
        {
            this.entity.Add(entity);
            _dbContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return entity.ToList();
        }

        public T GetById(int id)
        {
            return entity.Find(id);  // Använd entity istället för _context
        }

        public List<T> GetAllByUserName(string userName)
        {
            return entity.Where(t => userName.Equals(t.UserName)).ToList();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

    }
}