using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using FlagApi.SignalR;
namespace FlagApi
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
            services.AddSignalR();
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlagApi", Version = "v1" });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            string herokuConnectionString = $@"
                Server={Configuration["PostgreSql:Host"]};
                Port={Configuration["PostgreSql:Port"]};
                User Id={Configuration["PostgreSql:User"]};
                Password={Configuration["PostgreSql:ServerPassword"]};
                Database={Configuration["PostgreSql:DatabaseName"]};
                SSL Mode=Require;Trust Server Certificate=true";        
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(herokuConnectionString);           
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(builder.ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlagApi v1"));
            }            
            //app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
