﻿namespace BE.UI
{
    using BE.Comparer.Business;
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Service;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var server = @"Server=localhost\sqlexpress;Database=bibex_db;Trusted_Connection=True;";
            services.AddDbContext<BibleContext>(options => options.UseSqlServer(server));

            this.InitializeIOC(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            var indexDefault = "{{MainBible=1,OtherBibles=null,Book=1,Chapter=1}}";
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=BibleComparer}/{action=Index}/{bibleInfo=" + indexDefault + "}");
            });
        }

        public void InitializeIOC(IServiceCollection services)
        {
            services.AddTransient<IBibleService, BibleService>();
            services.AddTransient<ITextDiff, TextDiff>();
        }
    }
}