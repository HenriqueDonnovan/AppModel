using AppModel.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppModel
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add(item: "/Modulos/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add(item: "/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add(item: "/Views/Shared/{0}.cshtml");
            });

            services.AddDbContext<MeuDbContext>(options =>      
                options.UseSqlServer(Configuration.GetConnectionString("MeuDbContext"))
            );

            services.AddMvc(options =>

            options.EnableEndpointRouting = false

            ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddTransient<IPedidoRepository, PedidoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");

                routes.MapAreaRoute("AreaVendas", "Vendas", "Vendas/{controller=Pedidos}/{action=Index}/{id?}");

                routes.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");



            });
        }
    }
}
