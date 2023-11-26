using Microsoft.AspNetCore.HttpOverrides;
using Sicotyc.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCORS();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // Mostrado solo en modo DEV
}
else { 
    app.UseHsts(); // Agrega un Strict-Transport-Security header
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

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
