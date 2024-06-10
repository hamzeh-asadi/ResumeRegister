using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResumeRegister.datalayer.Context;
using System;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using ResumeRegister.core.Services;
using ResumeRegister.core.Services.Interfaces;

namespace ResumeRegister.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region Captcha service

            services.AddDNTCaptcha(options =>
                options.UseCookieStorageProvider()
                    .AbsoluteExpiration(minutes: 7)
                    .ShowThousandsSeparators(false)
                    .WithEncryptionKey("This is my secure key!")
                    .InputNames( // This is optional. Change it if you don't like the default names.
                        new DNTCaptchaComponent
                        {
                            CaptchaHiddenInputName = "DNTCaptchaText",
                            CaptchaHiddenTokenName = "DNTCaptchaToken",
                            CaptchaInputName = "DNTCaptchaInputText"
                        })
                    .Identifier("dntCaptcha")// This is optional. Change it if you don't like its default name.
                );

            #endregion
            #region Context
            services.AddDbContext<ResumeRegisterContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ResumeRegisterConnectionString"));
            });
            #endregion

            #region IoCs

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IResumeService, ResumeService>();


            #endregion

            #region Authentication

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.LoginPath = "/Login";
                option.LogoutPath = "/Logout";
                option.ExpireTimeSpan = TimeSpan.FromDays(15);
                
            });
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(10);
            });
            #endregion
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
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
