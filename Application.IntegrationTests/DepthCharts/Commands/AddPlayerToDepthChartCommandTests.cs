
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
        public async Task AddPlayerToDepthChart_WithInValidPositionId_ShouldThrowException()
        {
            //Arrange
            await CreateGameChart();
            var command = new AddPlayerToDepthChartCommand
            {
                PlayerId = 1,
                Position = "GJJSdksj"
            };

            //Act
            var ex = await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
        
            //Assert
            ex.And.Message.Should().Contain("Position not valid");

        }

    [Test]
    public async Task AddPlayerToDepthChart_WithValidCommand_ShouldAddPlayerToPosition()
    {
        //Arrange
        await CreateGameChart();

        var command = new AddPlayerToDepthChartCommand
        {
            PlayerId = 1,
            Position = "WR",
            PositionDepth = 3
        };

        //Act
        await FluentActions.Invoking(() =>
        SendAsync(command)).Invoke();
        var chart = await GetFullDepthChart();

        //Assert
        Assert.IsNotNull(chart);
        Assert.That(chart.Chart?.GetValueOrDefault("WR")?[2], Is.EqualTo(1));
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
