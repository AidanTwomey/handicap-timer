namespace finisher_service.lib;

public class Persister : IPersister
{
    private readonly TimeStamper timestamper;
    private readonly IDataStoreAdapter adapter;

    public Persister(TimeStamper timestamper, IDataStoreAdapter adapter)
    {
        this.adapter = adapter;
        this.timestamper = timestamper;
    }

    public async Task<bool> PersistFinishAsync()
    {
        var newPlace = await adapter.IncrementCurrentPlace();

        return await adapter.SaveFinish(
            newPlace.ToString(), 
            timestamper.NowSinceEpoch);
    }
}
