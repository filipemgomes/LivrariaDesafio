using Application.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Registro do serviço de relatório
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}
