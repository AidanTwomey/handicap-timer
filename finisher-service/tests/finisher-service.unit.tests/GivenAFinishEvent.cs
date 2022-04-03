using System.Threading.Tasks;
using finisher_service.lib;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace finisher_service.unit.tests;

public class GivenAFinishEvent
{
    private const int SecondsSinceEpoch = 1649018374;
    private const int Result = 5;

    [Fact]
    public async void When_Finish_Is_Persisted_To_Redis_Then_Timestamp_Is_Correct()
    {
        var timestamper = Substitute.For<TimeStamper>();
        timestamper.NowSinceEpoch.Returns(SecondsSinceEpoch);

        var adapter = Substitute.For<IDataStoreAdapter>();
        adapter.IncrementCurrentPlace().Returns(Task.FromResult(Result));
        
        adapter.SaveFinish(Arg.Any<string>(), Arg.Any<double>()).Returns(Task.FromResult(false));
        adapter.SaveFinish("5", SecondsSinceEpoch).Returns(Task.FromResult(true));

        var response = await new Persister(timestamper, adapter).PersistFinishAsync();

        response.Should().BeTrue();
    }
}