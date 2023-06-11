using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
            //services.AddDbContext<Context>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Category}/{action=Index}/{id?}");
            });
        }
    }
}
