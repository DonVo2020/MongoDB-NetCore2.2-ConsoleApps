using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bingo.Specification.IntegrationTests.Support
{
    public class TestStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Services
            services.AddTransient<IExercisesService, ExercisesService>();
            services.AddTransient<IMusclesService, MusclesService>();
            services.AddTransient<IActivationsService, ActivationsService>();

            // Register Repositories
            services.AddTransient<IExercisesRepository, ExercisesRepository>();
            services.AddTransient<IMusclesRepository, MusclesRepository>();
            services.AddTransient<IActivationsRepository, ActivationsRepository>();

            // Settings
            services.AddMvc();
            services.AddMvc().AddJsonOptions(opt => opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return settings;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        }

    }
}
