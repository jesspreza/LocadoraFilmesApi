using LocadoraFilmes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocadoraFilmes.Application.Mappings;
using LocadoraFilmes.Domain.Interfaces;
using LocadoraFilmes.Infra.Data.Repositories;
using LocadoraFilmes.Application.Interfaces;
using LocadoraFilmes.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using LocacaoFilmes.Domain.Account;
using LocadoraFilmes.Infra.Data.Identity;

namespace LocadoraFilmes.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<MSSQLContext>(options => 
            { 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly(typeof(MSSQLContext).Assembly.FullName));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["TokenConfigurations:Issuer"],
                    ValidAudience = configuration["TokenConfigurations:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["TokenConfigurations:Secret"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            //Repositories
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();

            //Services
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IFilmeService, FilmeService>();
            services.AddScoped<ILocacaoService, LocacaoService>();

            return services;
        }
    }
}
