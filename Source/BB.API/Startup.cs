using BB.Comparer.Business;
using BB.Infrastructure.Context;
using BB.Infrastructure.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BE.API
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

      this.InitializeIOC(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("Staging_2"))
      {
        app.UseExceptionHandler();
      }

      app.UseFileServer();
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    public void InitializeIOC(IServiceCollection services)
    {
      var bbConnectionString = this.Configuration.GetConnectionString("BB");
      services.AddDbContext<BibleContext>(options => options.UseSqlServer(bbConnectionString));

      services.AddTransient<IBibleService, BibleService>();
      services.AddTransient<ITextDiff, TextDiff>();
    }
  }
}
