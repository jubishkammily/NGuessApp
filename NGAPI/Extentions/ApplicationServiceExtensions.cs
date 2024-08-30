using Microsoft.EntityFrameworkCore;
using NGAPI.Data;
using NGAPI.Services;

namespace NGAPI.Extentions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();

            services.AddDbContext<DataContext>(opt =>
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }

    }
}
