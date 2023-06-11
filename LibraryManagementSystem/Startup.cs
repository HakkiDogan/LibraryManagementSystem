using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem
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

            //Tüm kullanýlan dependency injectionlar eklenmeli.
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddSingleton<ICategoryDal, EfCategoryDal>();

			services.AddTransient<IWriterService, WriterManager>();
			services.AddSingleton<IWriterDal, EfWriterDal>();

			services.AddTransient<IBookService, BookManager>();
			services.AddSingleton<IBookDal, EfBookDal>();

			services.AddTransient<IPublisherService, PublisherManager>();
			services.AddSingleton<IPublisherDal, EfPublisherDal>();

			services.AddTransient<IStaffService, StaffManager>();
			services.AddSingleton<IStaffDal, EfStaffDal>();

			services.AddTransient<IMemberService, MemberManager>();
			services.AddSingleton<IMemberDal, EfMemberDal>();

			services.AddTransient<IBookTransactionService, BookTransactionManager>();
			services.AddSingleton<IBookTransactionDal, EfBookTransactionDal>();

			services.AddSingleton<IPunishmentDal, EfPunishmentDal>();

			services.AddControllersWithViews();

            services.AddSession();

            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            services.AddMvc();

            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/auth/login";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
