namespace finisher_service.lib;

public interface IPersister
{
    Task<bool> PersistFinishAsync();
}
