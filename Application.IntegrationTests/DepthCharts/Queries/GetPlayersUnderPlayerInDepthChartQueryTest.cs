using Application.DepthCharts.Commands.AddPlayerToDepthChart;
using Application.DepthCharts.Queries.GetPlayersUnderPlayerInDepthChart;
using Domain.Entities;

namespace Application.IntegrationTests.DepthCharts.Queries;
using static Testing;

public class GetPlayersUnderPlayerInDepthChartQueryTest : BaseTestFixture
{
    [Test]
    public async Task GetPlayersUnderPlayerInDepthChart_ValidCommand_ShouldReturnSublist()
    {
        //Arrange
        await CreateGameChart();
        var bob = new Player(1, "Bob");
        var alice = new Player(2, "Alice ");
        var charlie = new Player(3, "Charlie");
        await AddPlayerToDepthChart(bob.Id, "WR", 0);
        await AddPlayerToDepthChart(alice.Id, "WR", 0);
        await AddPlayerToDepthChart(charlie.Id, "WR", 2);
        await AddPlayerToDepthChart(bob.Id, "KR");
        var query = new GetPlayersUnderPlayerInDepthChartQuery()
        {
            Id = alice.Id,
            Position = "WR"
        };
        
        //Act
        var subList = await FluentActions.Invoking(() =>
        SendAsync(query)).Invoke();

        //Assert
        Assert.IsNotNull(subList);
        Assert.That(subList.PlayerList, Is.EqualTo(new List<int>() { 1, 3 }));

    }
}

