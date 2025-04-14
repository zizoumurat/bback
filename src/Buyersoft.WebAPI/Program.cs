using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Helpers;
using Buyersoft.Infrastructure.Hubs;
using Buyersoft.Infrastructure.Services;
using Buyersoft.WebApi.Configurations;
using Buyersoft.WebApi.Middleware;
using Buyersoft.WebAPI.Converters;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterAssemblyTypes(
      Assembly.Load("Buyersoft.Domain"),
      Assembly.Load("Buyersoft.Application"),
      Assembly.Load("Buyersoft.Infrastructure"),
      Assembly.Load("Buyersoft.Persistance"))
    .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service"))
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://app.buyersoft.com", "http://localhost:4200", "http://localhost:4300")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});


// Add services to the container.

builder.Services
    .InstallServices(
    builder.Configuration, typeof(IServiceInstaller).Assembly);

builder.Services.AddSignalR();
builder.Services.AddScoped<ISendNotificationService, SendNotificationService>();

builder.Services.AddDataProtection();
builder.Services.AddSingleton<EncryptionHelper>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseDeveloperExceptionPage();

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/hub");

app.Run();
