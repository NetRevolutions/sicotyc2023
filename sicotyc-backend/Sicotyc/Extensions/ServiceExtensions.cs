using AspNetCoreRateLimit;
using Contracts;
using Entities.Models;
using FluentEmail.Smtp;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using Service.Contracts;
using Sicotyc.ActionFilters;
using System.Text;

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

        public static void ConfigureValidationFilterAttribute(this IServiceCollection services) =>
            services.AddScoped<ValidationFilterAttribute>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                { 
                    Endpoint = "*",
                    Limit = 30,
                    Period = "5m" // 5 minutos
                }
            };

            services.Configure<IpRateLimitOptions>(opt => 
            { 
                opt.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s => 
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                { 
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            { 
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });  
            });
        }

        public static void AddFluentEmail(this IServiceCollection services, ConfigurationManager configuration) 
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["SMTPSetting:Host"];
            var port = emailSettings.GetValue<int>("SMTPSetting:Port");
            var userName = emailSettings["SMTPSetting:UserName"];
            var password = emailSettings["SMTPSetting:Password"];


            //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            //{                
            //    EnableSsl = true,
            //    Port = port,
            //    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new System.Net.NetworkCredential(userName, password)
            //};

            

            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(host, port, userName, password);
                //.AddSmtpSender(smtp);
                

            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
