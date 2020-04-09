using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackOffice;
using BackOfficeAPI.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Z.EntityFramework.Extensions;

namespace BackOfficeAPI
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
            services.AddSession();
            services.AddScoped<DbContext, BackOfficeDBContext>();
            services.AddScoped<Itest, Test>();

            services.AddScoped<TransactionFilter>();
            services.AddDbContext<BackOfficeDBContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();              
            }

            app.UseMvc();
            app.UseSession();


        }
    }
}
