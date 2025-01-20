namespace CEORM.Data.Interfaces;

public interface IBaseRepository<T>
{
    void Save(T entity);
}