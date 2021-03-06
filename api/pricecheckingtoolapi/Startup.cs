using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using pricecheckingtoolapi.Db;
using pricecheckingtoolapi.Providers;
using pricecheckingtoolapi.Services;

namespace pricecheckingtoolapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var connection = "server=localhost;port=3306;database=pricecheckingtool;user=root;SslMode=none";
            services.AddDbContext<DatabaseContext>( // replace "YourDbContext" with the class name of your DbContext
                options => options.UseMySql(connection));
            services.AddHttpClient();
            services.AddSingleton<PricesProvider>();
            services.AddSingleton<IHostedService, PricesRefreshService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            using (loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                // use loggerFactory
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //var DB = app.ApplicationServices.GetRequiredService<DatabaseContext>();
            //DB.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
