using AspNetCore.Identity.MongoDbCore.Infrastructure;
using accomodation_service.Model;
using accomodation_service.Repository;
using accomodation_service.Service;
using accomodation_service.ProtoServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AccomodationDatabaseSettings>(
    builder.Configuration.GetSection("AccomodationDatabase"));

// add mongoIdentity configuration...
var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = "mongodb://accomodationdb:27017",
        DatabaseName = "AccomodationDB"
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

builder.Services.AddGrpc();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ListenAnyIP(5201, o => o.Protocols = HttpProtocols.Http2);
    options.ListenAnyIP(5200, o => o.Protocols = HttpProtocols.Http1);
});

builder.Services.AddSingleton<AccomodationRepository>();
builder.Services.AddSingleton<AccomodationService>();

builder.Services.AddSingleton<CreateAccomodation>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3100");
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcCheckAccomodationsService>();
});

app.Run();
