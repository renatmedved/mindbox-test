using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindboxTest.API.ModelBinders.Providers;
using MindboxTest.DAL.Connections;
using MindboxTest.DAL.QueryFactory;
using MindboxTest.Figures.Proxy;
using MindboxTest.Handlers.CalcArea;

namespace MindboxTest.API
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
            services.AddControllers(options => 
                options.ModelBinderProviders.Insert(0, new CustomBinderProvider()));
            
            ProxyFigureInitializator.Initialize();

            services.AddSingleton<ProxyFigureStorage>()
                .AddSingleton<ProxyFigureValidator>()
                .AddSingleton<ProxyFigureCalculator>()
                .AddSingleton<ProxyFigureDescriptionProvider>();

            services.AddSingleton<IQueryFactory, QueryFactory>();

            string conStr = Configuration.GetConnectionString("figures");

            services.AddMediatR(typeof(CalcAreaHandler));

            services.AddSingleton(_ => new ConnectionFactory(conStr));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
