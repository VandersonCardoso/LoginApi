using LoginApi.Core.Interfaces;
using LoginApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoginApi.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            #endregion

            return services;
        }
    }
}
