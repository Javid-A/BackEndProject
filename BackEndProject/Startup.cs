using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackEndProject
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		private readonly IConfiguration _config;
		public Startup(IConfiguration config)
		{
			_config = config;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(20);
			});

			services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
			 {
				 identityOptions.Password.RequireDigit = true;
				 identityOptions.Password.RequiredLength = 8;
				 identityOptions.Password.RequireNonAlphanumeric = true;
				 identityOptions.Password.RequireLowercase = true;
				 identityOptions.Password.RequireUppercase = true;

				 identityOptions.Lockout.MaxFailedAccessAttempts = 3;
				 identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				 identityOptions.Lockout.AllowedForNewUsers = true;
			 }
			).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			services.AddAuthorization(options =>
			{
				options.AddPolicy("CourseManager", policy => policy.RequireAssertion(context =>
					   context.User.IsInRole(Helpers.Helper.Roles.Admin.ToString()) || context.User.IsInRole(Helpers.Helper.Roles.CourseOwner.ToString())
				 ));
			});
			
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(_config["ConnectionStrings:DefaultConnection"])
			);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			//else
			//{
			//	app.UseExceptionHandler("/Home/Error");
			//	app.UseHsts();
			//}
			app.UseStaticFiles();
			app.UseSession();
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
				);
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{Id?}"
				);
			});
		}
	}
}
