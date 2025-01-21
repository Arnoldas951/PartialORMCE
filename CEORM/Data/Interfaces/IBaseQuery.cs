namespace CEORM.Data.Interfaces;

public interface IBaseQuery<T>
{
    T FirstOrDefault(int id);
    T FirstOrDefault();
    List<T> ToList();
    List<T> ToList(int id);
}