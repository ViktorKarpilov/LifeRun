using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AutoMapper;
using UserManagement.API.Extentions;
using UserManagement.Application.Helpers;
using UserManagement.Persistence.Extentions;
using UserManagement.API.Validators;
using FluentValidation.AspNetCore;

namespace LunchMe.Host
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //here we will add db context

            // Adding cors-services for requests from different domains
            services.AddCors();
            services.AddControllers()
                .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<UpdateRequestValidator>();
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.RegisterPersistence(_configuration);
            services.RegisterAPI();
            services.Configure<AuthenticationSettings>(_configuration.GetSection("AuthenticationSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}