using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CrudTest.Data;
using CrudTest.Models;
using CrudTest.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
namespace CrudTest.Models
{
    public static class DependencyInjection
    {
        public static void AddRepository(IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ILibraryRepository, LibraryRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }
    }
}
