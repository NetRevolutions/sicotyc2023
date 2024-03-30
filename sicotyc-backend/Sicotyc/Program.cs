using Microsoft.AspNetCore.HttpOverrides;
using Sicotyc.Extensions;
using NLog;
using Contracts;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCORS();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSearchService();
builder.Services.ConfigureUploadFileService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.ConfigureValidationFilterAttribute();
builder.Services.ConfigureValidationTokenFilter();

//builder.Services.AddControllers(); // Por defecto solo devuelve en text/json

// Hacer que devuelve en otros formatos
builder.Services.AddControllers(config => { 
    config.RespectBrowserAcceptHeader = true; // Con esto habilito otros tipos de respuestas (ej. text/xml) 
    config.ReturnHttpNotAcceptable = true; // Con esto retrinjo respuestas aceptables (ej: text/json, text/xml
    //config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120});
}).AddXmlDataContractSerializerFormatters(); // Serializo en XML

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.AddFluentEmail(builder.Configuration);
builder.Services.AddHttpClient();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.UseDeveloperExceptionPage(); // Mostrado solo en modo DEV
    // Lo comentamos porque vamos a manejarlo con nuestro middleware de errores personalizado.
}
else if (app.Environment.IsProduction()) { 
    app.UseHsts(); // Agrega un Strict-Transport-Security header
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
//app.UseIpRateLimiting();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => 
{ 
    endpoints.MapControllers();
});

/*
app.Use(async (context, next) => {
    Console.WriteLine($"Logica antes de usar el delegado next en el metodo Use");
    //await context.Response.WriteAsync("Hola desde mi middleware component"); // (1.)
    await next.Invoke();
    Console.WriteLine($"Logica despues de ejecutar el delegado next en el metodo Use");
});
app.Map("/usingmapbranch", builder => {
    builder.Use(async (context, next) => {
        Console.WriteLine("Logica de branch Map en el metodo Use antes del delegado next");
        await next.Invoke();
        Console.WriteLine("Logica de branch Map en el metodo Use despues del delegado next");
    });
    builder.Run(async context => { 
        Console.WriteLine($"Response del branch Map hacia el cliente en el metodo Run");
        await context.Response.WriteAsync("Hola desde el branch Map");
    });
});
app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), builder => {
    builder.Run(async context => {
        await context.Response.WriteAsync("Hola desde el branch MapWhen");
    });
});
*/
/*
app.Run(async context => {
    Console.WriteLine($"Escribiendo el response hacia el cliente en el metodo Run");
    //context.Response.StatusCode = 200; // Generara error si es que existe antes un context.Response...(1.)
    await context.Response.WriteAsync("Hola desde mi middleware component");
});
*/

app.MapControllers();

app.Run();
