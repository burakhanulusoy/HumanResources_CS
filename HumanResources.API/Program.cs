using HumanResources.Business.BackgroundJobs;
using HumanResources.Business.Extensions;
using HumanResources.Business.Mappings;
using HumanResources.DataAccess.Extensions;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddRepositoriesExt(builder.Configuration)
                .AddServiceExt(builder.Configuration);

TypeAdapterConfig.GlobalSettings.Scan(typeof(UnitMappingConfig).Assembly);
builder.Services.AddHostedService<CertificateStatusUpdateJob>();

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
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
