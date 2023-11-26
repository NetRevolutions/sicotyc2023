using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace Sicotyc.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCORS(this IServiceCollection services) =>
            services.AddCors(options => { 
                options.AddPolicy("CorsPolicy", builder => 
                builder.AllowAnyOrigin() //Despues usaremos esto: WithOrigins("https://example.com")
                .AllowAnyMethod() //Despues podemos usar esto: WithMethods("POST", "GET")
                .AllowAnyHeader()); //Despues podemos usar esto: WithHeaders("accept", "content-type")
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => {
                // Usado para hostear la aplicacion en IIS

                // options.AutomaticAuthentication = true; 
                /*
                 * Si es true el middleware configura el HttpContext.User
                 * Si es false el middleware proporciona un identity HttpContext.User y responde a los cambios de
                 * AuthenticationScheme; Windows Authentication debe de ser habilitado en IIS que AutomaticAuthentication funcione
                 */

                // options.AuthenticationDisplayName = null;
                /*
                 * Usado para configurar el nombre a mostrar a los usuarios en las paginas de login 
                 */

                // options.ForwardClientCertificate = true;
                /*
                 * Si es true y el MS-ASPNETCORE-CLIENTCERT request header esta presente, el 
                 * HttpContext.Connection.ClientCertificate es llenado
                 */

            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) => 
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }
}
