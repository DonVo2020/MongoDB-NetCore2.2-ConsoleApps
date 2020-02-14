using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using System.Linq;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ControllerTests
{
    [Trait("Api", nameof(StartupTests))]
    public class StartupTests
    {
        #region ConfigureServices(IServiceCollection services)

        [Fact]
        public void ConfigureServices_ShouldRegisterExercisesRepository_WhenCalled()
        {
            // Arrange
            var startup = new Startup();
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert
            services.ToList().ShouldContain(service => service.ServiceType == typeof(IExercisesRepository));
        }

        [Fact]
        public void ConfigureServices_ShouldRegisterExercisesService_WhenCalled()
        {
            // Arrange
            var startup = new Startup();
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert
            services.ToList().ShouldContain(service => service.ServiceType == typeof(IExercisesService));
        }

        [Fact]
        public void ConfigureServices_ShouldRegisterMusclesRepository_WhenCalled()
        {
            // Arrange
            var startup = new Startup();
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert
            services.ToList().ShouldContain(service => service.ServiceType == typeof(IMusclesRepository));
        }

        [Fact]
        public void ConfigureServices_ShouldRegisterMusclesService_WhenCalled()
        {
            // Arrange
            var startup = new Startup();
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert
            services.ToList().ShouldContain(service => service.ServiceType == typeof(IMusclesService));
        }

        [Fact]
        public void ConfigureServices_ShouldRegisterActivationsRepository_WhenCalled()
        {
            // Arrange
            var startup = new Startup();
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert
            services.ToList().ShouldContain(service => service.ServiceType == typeof(IActivationsRepository));
        }

        [Fact]
        public void ConfigureServices_ShouldRegisterActivationsService_WhenCalled()
        {
            // Arrange
            var startup = new Startup();
            var services = new ServiceCollection();

            // Act
            startup.ConfigureServices(services);

            // Assert
            services.ToList().ShouldContain(service => service.ServiceType == typeof(IActivationsService));
        }

        #endregion
    }
}
