using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLOG.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BLOG
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<BLOG.Data.BlogPostsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

            services.AddScoped(typeof(IRepository<>),typeof(DataRepository<>));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp2/dist";

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();           
            app.UseMvc();


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp2";
                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
