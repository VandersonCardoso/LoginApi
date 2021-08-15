using FluentValidation.AspNetCore;
using LoginApi.Core.Validations;
using LoginApi.Data.Contexts;
using LoginApi.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace LoginApi.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        #region Startup
        public Startup(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        #endregion

        #region ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UsuarioSelfValidation>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestSelfValidation>());

            #region Contexts
            services.AddDbContext<LoginDbContext>(opt => opt.UseInMemoryDatabase("LoginProvider"));
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Login e-Commerce",
                    Description = "Esta API tem como objetivo disponibilizar endpoints para manipulação de usuários de um e-Commerce.",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ConfigureIoC(services);
        }
        #endregion

        #region Configure
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureSwagger(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        #endregion

        #region ConfigureIoC
        private void ConfigureIoC(IServiceCollection services)
        {
            services.ResolveDependencies();
        }
        #endregion

        #region ConfigureSwagger
        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "V1");
                c.DocumentTitle = "Api Login e-Commerce";
                c.DocExpansion(DocExpansion.None);
            });
        }
        #endregion
    }
}
