using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateManagement.Models;
using CandidateManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateManagement
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<CandidateManagementContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("CandidateManagement")));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<CandidateManagementContext>()
            .AddDefaultTokenProviders();
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });            
            services.AddAuthorization(options => 
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "488470500583-j6amaim72mr1kgqkisq1juole1ttjmtk.apps.googleusercontent.com";
                options.ClientSecret = "SnNRURGi2g40kVY70kVFq_9n";
            });
            // Set token life span to 5 hours
            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(5));
            services.AddTransient<IAbilityRepository, MockAbilityRepository>();
            services.AddTransient<IApplyPositionRepository, MockApplyPositionRepository>();
            services.AddTransient<IApplyRepository, MockApplyRepository>();
            services.AddTransient<IApplyDetailRepository, MockApplyDetailRepository>();
            services.AddTransient<IApplyPositionAbilityRepository, MockApplyPositionAbilityRepository>();
            services.AddTransient<IApplyDetailAbilityRepository, MockApplyDetailAbilityRepository>();
            services.AddTransient<ICandidateRepository, MockCandidateRepository>();
            services.AddTransient<ICollegeRepository, MockCollegeRepository>();
            services.AddTransient<IInterviewResultRepository, MockInterviewResultRepository>();
            services.AddTransient<IInterviewScheduleRepository, MockInterviewScheduleRepository>();
            services.AddTransient<IOperatorRepository, MockOperatorRepository>();
            services.AddTransient<ISavedRequirementRepository, MockSavedRequirementRepository>();
            services.AddTransient<IRequirementRepository, MockRequirementRepository>();
            services.AddTransient<IRequiredAbilityRepository, MockRequiredAbilityRepository>();
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            //services.AddSingleton<IMailer, Mailer>();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });            
        }
    }
}
