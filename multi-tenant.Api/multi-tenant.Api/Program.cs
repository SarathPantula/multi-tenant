using core.Extensions.StartUpExtensions;
using Microsoft.EntityFrameworkCore;
using multi_tenant.Api.Extensions;
using multi_tenant.Api.Extensions.StartupExtensions;
using multi_tenant.Api.Filters.ActionFilters;
using multi_tenant.Models.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((hostName, configuration) =>
{
    builder.Services.RegisterSerilogLogging(builder.Configuration, hostName, configuration);
});

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.RegisterStartupExtensions(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<DbContext, MultiTenantContext>().AddDbContext<MultiTenantContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLDbContext")));

builder.Services.ConfigureFilterExtensions();

builder.Services.RegisterAPIServices();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Multi Tenant v1");
});

app.UseSerilogRequestLogging();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    await next.Invoke();
});

app.UseMiddleware<TenantFilter>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder => builder.WithOrigins("*")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
