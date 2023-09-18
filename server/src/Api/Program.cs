using System.Text;
using ChatServer.Api;
using ChatServer.Database;
using ChatServer.Database.Settings;
using ChatServer.Api.Settings;
using ChatServer.Api.Utils.AppRegistration;
using ChatServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigFile("appsettings.local.json");
builder.AddAutofac(
    new ApiModule(),
    new ServicesModule(),
    new DatabaseModule(builder.Configuration));
builder.ConfigureSettings<MongoSettings>(MongoSettings.Section);
builder.ConfigureSettings<GoogleAuthSettings>(GoogleAuthSettings.Section);
builder.ConfigureSettings<JwtSettings>(JwtSettings.Section);
builder.AddDefaultAllowAllCors();
builder.AddControllers();
builder.AddSwagger();
builder.AddSignalR();
builder.AddIdentity();

#region raw

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(24);
});
// builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
// builder.Services.AddScoped<IAuthService, AuthService>();
var jwtSection = builder.Configuration.GetSection("JWT");
builder.Services.Configure<JwtSettings>(jwtSection);

var appSettings = jwtSection.Get<JwtSettings>();
var secret = Encoding.ASCII.GetBytes(appSettings!.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = true;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = appSettings.ValidIssuer,
        ValidAudience = appSettings.ValidAudience,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(secret)
    };

});

#endregion

var app = builder.Build();
app.UseSwaggerDoc();
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapSignalRHubs();

app.Run();


