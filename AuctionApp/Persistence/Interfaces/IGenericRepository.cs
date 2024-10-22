namespace AuctionApp.Persistence.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    List<T> GetAll();
    
    List<T> GetAllByUserName(string userName);
    
    T GetById(int id);
    
    void Create(T entity);
    
    void Update(T entity);

}