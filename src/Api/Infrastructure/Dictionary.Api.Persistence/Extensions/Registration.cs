using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Api.Persistence.Context;
using Dictionary.Api.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DictionaryContext>(conf =>
        {
            var connStr = configuration["DictionaryConnectionString"].ToString();
            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        ///This code is not always used because the program generates fake data every time it runs
        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

        return services;
    }
}
