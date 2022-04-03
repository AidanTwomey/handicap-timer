namespace finisher_service.lib;

public class TimeStamper
{
    public virtual double NowSinceEpoch => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
