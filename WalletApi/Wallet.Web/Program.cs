using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wallet.Application;
using Wallet.Application.Features.AccountFeatures.Services;
using Wallet.Core.Entities;
using Wallet.Infrastructure;
using Wallet.Infrastructure.UnitOfWork.Abstract;
using Wallet.Infrastructure.UnitOfWork;
using Wallet.Web;
using Wallet.Web.Extensions;
using Wallet.Web.Middlewares.ExceptionHandlingMiddleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using Wallet.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<UserEntity, RoleEntity>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddBearer(builder.Configuration.GetValue<string>("Jwt:Secret")!);

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
       // to ingore loop inside entities that reference each other
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
   );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddHangfire(x =>
    x.UsePostgreSqlStorage(options => 
        options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("Default"))));

builder.Services.AddHangfireServer();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(FluentValidationAssemblyReference), ServiceLifetime.Transient);

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(MediatrAssemblyReference).Assembly));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSeed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<PointsService>("UpdatePointsJob", x => x.AddPointsAsync(), @"0 4 * * *");

app.Run();
