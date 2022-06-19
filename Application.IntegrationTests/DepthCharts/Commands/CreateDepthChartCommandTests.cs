
using Application.Common.Enums;
using Application.DepthCharts.Commands.CreateDepthChart;
using Domain.Common.Exceptions;
using Domain.Entities;
using Application.DepthCharts.Commands.AddPlayerToDepthChart;
using System.ComponentModel.DataAnnotations;


namespace Application.IntegrationTests.DepthCharts.Commands;
using static Testing;
public class CreateDepthChartsTests : BaseTestFixture
        {

            [Test]
            public async Task CreateDepthChart_WithInValidGameType_ShouldThrowException()
            {
                //Arrange
                var command = new CreateDepthChartCommand
                {
                    GameType = GameTypes.None
                };

                //Act
               var ex = await FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().ThrowAsync<DomainUnprocessableException>();

                //Assert
                ex.And.Message.Should().Be("Game type not supported.");

            }

            [Test]
            public async Task CreateDepthChart_WithNflGameType_ShouldCreateNflDepthChart()
            {
                //Arrange
                var command = new CreateDepthChartCommand
                {
                    GameType =GameTypes.NFL
                };

                //Act
                var chart = await FluentActions.Invoking(async () =>
                await SendAsync(command)).Invoke();

                //Assert
                Assert.IsTrue(chart != null);
                Assert.That(chart.GameType, Is.EqualTo(GameTypes.NFL.ToString()));
                Assert.That(chart.GetType().Name, Is.EqualTo(typeof(NflDepthChart).Name));
                Assert.That(chart.Chart.Count(), Is.EqualTo(8));
    }

            [Test]
            public async Task CreateDepthChart_WithMlbGameType_ShouldCreateMlbDepthChart()
            {
                //Arrange
                var command = new CreateDepthChartCommand
                {
                    GameType = GameTypes.MLB
                };

                //Act
                var chart = await FluentActions.Invoking(async () =>
                await SendAsync(command)).Invoke();

                //Assert
                Assert.IsTrue(chart != null);
                Assert.That(chart.GameType, Is.EqualTo(GameTypes.MLB.ToString()));
                Assert.That(chart.GetType().Name, Is.EqualTo(typeof(MlbDepthChart).Name));
                Assert.That(chart.Chart.Count(), Is.EqualTo(11));
    }
    
    }
