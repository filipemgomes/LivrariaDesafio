using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace API.DependencyInjection
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();

            return services;
        }
    }
}
