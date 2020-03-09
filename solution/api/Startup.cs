using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using pecacompativel.db.Models;
using pecacompativel.db.Services;

namespace pecacompativel.api
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
            // requires using Microsoft.Extensions.Options
            services.Configure<PecaCompativelDatabaseSettings>(
                Configuration.GetSection(nameof(PecaCompativelDatabaseSettings)));

            services.AddSingleton<IPecaCompativelDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PecaCompativelDatabaseSettings>>().Value);

            services.AddSingleton<PecaService>();
            services.AddSingleton<MarcaService>();
            services.AddSingleton<ModeloService>();

            services.AddCors();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/dev");

            //app.UseCors( b =>
            //{
            //    b.WithOrigins("http://127.0.0.1:5500/", "http://localhost/");
            //});
            //app.UseCors(x => x.AllowAnyHeader());
            //app.UseCors(x => x.AllowAnyMethod());
            //app.UseCors(option => option.AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option => option
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
