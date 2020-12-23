using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Login;
using Identity.Login.Context;
using Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity
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
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            services.AddScoped<IUserService, UserService>();
            services.AddTransient<ILogin, Login.LoginService.UserService>();
            services.AddDbContext<LoginContext>(temp=>temp.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

    private static void ApplyDatabaseMigrations(IApplicationBuilder app)
    {
    using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

    scope.ServiceProvider.GetRequiredService<LoginContext>().Database.Migrate();
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                ApplyDatabaseMigrations(app);
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
