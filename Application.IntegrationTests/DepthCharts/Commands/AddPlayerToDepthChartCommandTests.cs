
using Application.Common.Enums;
using Application.DepthCharts.Commands.AddPlayerToDepthChart;
using Application.DepthCharts.Commands.CreateDepthChart;
using Application.DepthCharts.Queries.GetFullDepthChart;
using Domain.Entities;
using FluentValidation;

namespace Application.IntegrationTests.DepthCharts.Commands;

    using static Testing;
    public class AddPlayerToDepthChartCommandTests : BaseTestFixture
    {

        [Test]
        public async Task AddPlayerToDepthChart_WithInValidCommand_ShouldThrowException()
        {
            //Arrange
        await CreateGameChart();
        var command = new AddPlayerToDepthChartCommand
            {

            };

        //Act
        var ex = await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
            //Assert
            ex.And.Message.Should().Contain("PlayerId is required");

        }

    [Test]
    [TestCase("JKHWR")]
    [TestCase("JKHQB")]
    [TestCase("JKHRB")]
    [TestCase("JKHTE")]
    [TestCase("JKHK")]
    [TestCase("JKHP")]
    [TestCase("JKHKR")]
    [TestCase("JKHPR")]
    [TestCase("JK454HWR")]
    [Parallelizable(ParallelScope.All)]
    public async Task AddPlayerToDepthChart_WithInValidPositionId_ShouldThrowException(string position)
        {
            //Arrange
            await CreateGameChart();
            var command = new AddPlayerToDepthChartCommand
            {
                PlayerId = 1,
                Position = position
            };

            //Act
            var ex = await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
        
            //Assert
            ex.And.Message.Should().Contain("Position not valid");

        }

    [Test]
    [TestCase(1, "WR", 3)]
    [TestCase(1, "QB", 10)]
    [TestCase(1, "RB", 2)]
    [TestCase(1, "TE", 4)]
    [TestCase(1, "K", 5)]
    [TestCase(1, "P", 6)]
    [TestCase(1, "KR", 7)]
    [TestCase(1, "PR", 8)]
    [TestCase(1, "WR", 9)]
    public async Task AddPlayerToDepthChart_WithValidCommand_ShouldAddPlayerToPosition(int playerId, string position, int positionDepth)
    {
        //Arrange
        await CreateGameChart();

        var command = new AddPlayerToDepthChartCommand
        {
            PlayerId = playerId,
            Position = position,
            PositionDepth = positionDepth
        };

        //Act
        await FluentActions.Invoking(() =>
        SendAsync(command)).Invoke();
        var chart = await GetFullDepthChart();

        //Assert
        Assert.IsNotNull(chart);
        Assert.That(chart.Chart?.GetValueOrDefault(position)?[positionDepth - 1], Is.EqualTo(playerId));
    }

    [Test]
    public async Task AddPlayerToDepthChart_WitSamePosition_ShouldAddPlayerToPosition()
    {
        //Arrange
        await CreateGameChart();
        var bob = new Player(1, "Bob");
        var alice = new Player(2, "Alice ");
        var charlie = new Player(3, "Charlie");
        var command1 = new AddPlayerToDepthChartCommand
        {
            PlayerId = bob.Id,
            Position = "WR",
            PositionDepth = 0
        };
        var command2 = new AddPlayerToDepthChartCommand
        {
            PlayerId = alice.Id,
            Position = "WR",
            PositionDepth = 0
        };
        var command3 = new AddPlayerToDepthChartCommand
        {
            PlayerId = charlie.Id,
            Position = "WR",
            PositionDepth = 2
        };
        var command4 = new AddPlayerToDepthChartCommand
        {
            PlayerId = bob.Id,
            Position = "KR"
        };

        //Act
        await FluentActions.Invoking(() =>
        SendAsync(command1)).Invoke();
        await FluentActions.Invoking(() =>
        SendAsync(command2)).Invoke();
        await FluentActions.Invoking(() =>
        SendAsync(command3)).Invoke();
        await FluentActions.Invoking(() =>
        SendAsync(command4)).Invoke();

        var chart = await GetFullDepthChart();

        //Assert
        Assert.IsNotNull(chart);
        Assert.That(chart.Chart?.GetValueOrDefault("WR"), Is.EqualTo(new List<int>() { 2, 1, 3 }));
        Assert.That(chart.Chart?.GetValueOrDefault("KR"), Is.EqualTo(new List<int>() { 1 }));
        Assert.That(chart.Chart.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task AddPlayerToDepthChart_WitOutOfRangePosition_ShouldAddPlayerToPosition()
    {
        //Arrange
        await CreateGameChart();
        var bob = new Player(1, "Bob");
        var alice = new Player(2, "Alice ");
        var command1 = new AddPlayerToDepthChartCommand
        {
            PlayerId = bob.Id,
            Position = "WR",
            PositionDepth = 0
        };
        var command2 = new AddPlayerToDepthChartCommand
        {
            PlayerId = alice.Id,
            Position = "WR",
            PositionDepth = 6
        };
        //Act
        await FluentActions.Invoking(() =>
        SendAsync(command1)).Invoke();
        await FluentActions.Invoking(() =>
        SendAsync(command2)).Invoke();

        var chart = await GetFullDepthChart();

        //Assert
        Assert.IsNotNull(chart);
        Assert.That(chart.Chart?.GetValueOrDefault("WR"), Is.EqualTo(new List<int>() { 1, 0, 0, 0, 0, 0, 2 }));
        Assert.That(chart.Chart.Count(), Is.EqualTo(1));
    }
    
}
