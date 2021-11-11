using HallOfFame.Data;
using HallOfFame.Data.Interfaces;
using HallOfFame.Logic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using HallOfFame.Logic;
using HallOfFame.Common;
using Microsoft.Extensions.Logging;

namespace HallOfFame.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HallOfFame", Version = "v1" });
            });
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HallOfFameDbContext>(options => options.UseSqlServer(connection));
            services.AddSingleton<ILogger, FileLogger>();
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IDtoService, DtoService>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HallOfFame v1"));
            }

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
