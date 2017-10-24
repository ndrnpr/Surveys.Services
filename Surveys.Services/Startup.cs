using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Surveys.Data.Tests;
using Surveys.Data;
using Microsoft.EntityFrameworkCore;
using Surveys.Data.Repository;
using Surveys.Data.Repository.Interfaces;

namespace Surveys.Services
{
    public class Startup
    {

        private IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SurveysContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("SurveysDB")
                )
            );
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<IChoiceRepository, ChoiceRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IBulletinRepository, BulletinRepository>();            
            services.AddMvc()
                .AddJsonOptions(
                    options => 
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();

                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    SurveysInitializer.InitializeData(app.ApplicationServices);
                }
            }

            app.UseMvc();
        }
    }
}
