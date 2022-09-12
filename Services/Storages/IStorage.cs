namespace Services.Storages
{
    public interface IStorage<T>
    {
        void Add(T item);
        void Delete(T item);
        void Update(T item);
    }
}
