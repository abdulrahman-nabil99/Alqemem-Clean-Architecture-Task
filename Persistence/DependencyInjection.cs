using CleanArchTask.Application.Behaviors;
using CleanArchTask.Application.Features.Employee.Queries.GetByIdQuery;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Persistence.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IEmployeeRepository, EmployeeRepositoryTest>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetEmployeeByIdQuery).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(GetEmployeeByIdQuery).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
