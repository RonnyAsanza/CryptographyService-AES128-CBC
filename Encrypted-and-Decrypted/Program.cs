using Encrypted_and_Decrypted;
using Encrypted_and_Decrypted.NewFolder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceProvider providers = builder.Services.BuildServiceProvider();
var configuration = providers.GetRequiredService<IConfiguration>();
builder.Services.AddLocalization();
builder.Services.AddRequestLocalization(x =>
{
    x.DefaultRequestCulture = new RequestCulture("en");
    x.ApplyCurrentCultureToResponseHeaders = true;
    x.SupportedCultures = new List<CultureInfo> { new("es"), new("en") };
    x.SupportedUICultures = new List<CultureInfo> { new("es"), new("en") };
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder
        .SetIsOriginAllowed(hostName => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        );
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
builder.Services.AddAuthorization();
DataConfiguration.Configure(builder.Services);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "QA")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
