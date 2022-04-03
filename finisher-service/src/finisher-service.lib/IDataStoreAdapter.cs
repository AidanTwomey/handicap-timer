namespace finisher_service.lib;

public interface IDataStoreAdapter
{
    Task<int> IncrementCurrentPlace();
    Task<bool> SaveFinish(string place, double timestamp);
}
