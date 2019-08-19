﻿
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UWay.Skynet.Cloud.Extensions;
using Steeltoe.Security.Authentication.CloudFoundry;
using UWay.Skynet.Cloud.Mvc;
namespace Skynet.Cloud.Cloud.CloudFoundryDemo
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCloudFoundryJwtBearer(Configuration);
            services.AddAuthorization(options => {
                options.AddPolicy("Values", policy => policy.RequireClaim("values.me"));
            });
            services.AddDiscoveryClient(Configuration);
            //services.AddMySwagger();
            services.UseMysql(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseAuthentication();
            app.UseDiscoveryClient();
            //app.UseJwtBearerAuthentication()
            //app.UseHttpsRedirection();
            //app.UseSwagger();
            //app.UseSwaggerUi3();
            app.UseMvc();
            
        }
    }
}
