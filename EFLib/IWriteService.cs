namespace EFLib
{
    public interface IWriteService<in TEntity> where TEntity : class, IObjectWithState
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void DeleteById(int id);
    }
}
