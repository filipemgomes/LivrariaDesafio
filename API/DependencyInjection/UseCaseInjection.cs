using Application.UseCases.Reports.GetBooksWithAuthors;

namespace API.DependencyInjection
{
    public static class UseCaseInjection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
                        services.AddScoped<IGetBooksWithAuthorsUseCase, GetBooksWithAuthorsUseCase>();

            return services;
        }
    }
}
