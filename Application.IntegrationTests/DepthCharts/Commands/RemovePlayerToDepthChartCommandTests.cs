
using Application.Common.Enums;
using Application.DepthCharts.Commands.CreateDepthChart;
using Application.DepthCharts.Commands.RemovePlayerFromDepthChart;
using Application.DepthCharts.Queries.GetFullDepthChart;
using Domain.Common.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.IntegrationTests.DepthCharts.Commands;

    using static Testing;
    public class RemovePlayerToDepthChartCommandTests : BaseTestFixture
    {

        [Test]
        public async Task RemovePlayerToDepthChart_WithInValidCommand_ShouldThrowException()
        {
            //Arrange
        var command = new RemovePlayerFromDepthChartCommand
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
    [Parallelizable(ParallelScope.Self)]
    public async Task RemovePlayerToDepthChart_WithInValidPositionId_ShouldThrowException(string position)
        {
            //Arrange
            await CreateGameChart();
            await AddPlayerToDepthChart(1, "WR");
            var command = new RemovePlayerFromDepthChartCommand
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
    public async Task RemovePlayerToDepthChart_WithValidCommand_ShouldRemovePlayerToPosition(int playerId, string position, int positionDepth)
    {
        //Arrange
        await CreateGameChart();
        await AddPlayerToDepthChart(playerId, position);
        var command = new RemovePlayerFromDepthChartCommand
        {
            PlayerId = playerId,
            Position = position
        };

        //Act
        await FluentActions.Invoking(() =>
        SendAsync(command)).Invoke();
        var chart = await GetFullDepthChart();

        //Assert
        Assert.IsNotNull(chart);
        Assert.That(chart.Chart?.Count(), Is.EqualTo(0));
    }
    
}
