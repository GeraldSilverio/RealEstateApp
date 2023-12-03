using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using System.Reflection;

namespace RealEstateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITypeOfRealEstateService, TypeOfRealEstateService>();
            services.AddTransient<ITypeOfSaleService, TypeOfSaleService>();
            services.AddTransient<IImprovementService, ImprovementService>();
            services.AddTransient<IRealEstateService, RealEstateService>();
            #endregion
        }
    }
}