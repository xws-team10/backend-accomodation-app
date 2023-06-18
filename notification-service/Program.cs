using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using notification_service.Model;
using notification_service.ProtoServices;
using notification_service.Repository;
using notification_service.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<NotificationsDatabaseSettings>(
    builder.Configuration.GetSection("NotificationsDatabase"));

var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = "mongodb://notificationdb:27017",
        DatabaseName = "NotificationsDB"
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

builder.Services.AddSingleton<NotificationUserSettingsRepository>();
builder.Services.AddSingleton<NotificationRepository>();

builder.Services.AddSingleton<NotificationUserSettingsService>();
builder.Services.AddSingleton<NotificationService>();

builder.Services.AddGrpc();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    //options.ConfigureEndpointDefaults(lo => lo.Protocols = HttpProtocols.Http1AndHttp2);
    options.ListenAnyIP(5401, o => o.Protocols = HttpProtocols.Http2);
    options.ListenAnyIP(5400, o => o.Protocols = HttpProtocols.Http1);
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
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcReservationNotificationsService>();
    endpoints.MapGrpcService<GrpcAccountNotificationService>();
    endpoints.MapGrpcService<GrpcAccomodationNotificationsService>();
});

app.Run();
