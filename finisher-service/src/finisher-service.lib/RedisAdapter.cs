using StackExchange.Redis;

namespace finisher_service.lib;

public sealed class RedisAdapter : IDataStoreAdapter
{
    private readonly IDatabase db;

    private static ConfigurationOptions configuration = new ConfigurationOptions
        {
            EndPoints = { Environment.GetEnvironmentVariable("redis_connection") }
        };

    public RedisAdapter()
    {
        db = ConnectionMultiplexer.Connect(configuration).GetDatabase();
    }

    public async Task<int> IncrementCurrentPlace()
    {
        var currentPlace = await db.StringGetAsync("current");

        var newPlace = string.IsNullOrEmpty(currentPlace) 
            ? 1
            : Convert.ToInt32(currentPlace) + 1;

        await db.StringSetAsync("current", newPlace.ToString());

        return newPlace;
    }

    public async Task<bool> SaveFinish(string place, double timestamp)
    {
        return await db.StringSetAsync(place, timestamp);
    }
}
