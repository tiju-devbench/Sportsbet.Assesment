namespace Application.IntegrationTests
{
    using Application.Common.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Moq;
    using System;
    using static Testing;

    [TestFixture]
    public abstract class BaseTestFixture
    {
        //public IGameService _gameService;
        public IServiceProvider _serviceProvider;

        [SetUp]
        public async Task TestSetUp()
        {
            
        }
    }
}