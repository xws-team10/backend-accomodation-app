using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using reservation_service.Model;
using reservation_service.ProtoServices;
using reservation_service.Repository;
using reservation_service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ReservationsDatabaseSettings>(
    builder.Configuration.GetSection("ReservationsDatabase"));

// add mongoIdentity configuration...
var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = "mongodb://reservationdb:27017",
        DatabaseName = "ReservationsDB"
    },
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;

        //lockout
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.MaxFailedAccessAttempts = 5;

        options.User.RequireUniqueEmail = true;
    }
};

builder.Services.AddSingleton<ReservationRequestRepository>();
builder.Services.AddSingleton<ReservationRequestService>();

builder.Services.AddSingleton<ReservationRepository>();
builder.Services.AddSingleton<ReservationService>();
builder.Services.AddSingleton<CheckAccomodations>();

builder.Services.AddGrpc();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ConfigureEndpointDefaults(lo => lo.Protocols = HttpProtocols.Http1AndHttp2);
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
    //opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:5000");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcSearchService>();
    endpoints.MapGrpcService<GrpcCheckService>();
});

app.Run();
