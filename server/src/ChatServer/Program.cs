using ChatServer;
using ChatServer.Database;
using ChatServer.Database.Settings;
using ChatServer.Settings;
using ChatServer.Utils.AppRegistration;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigFile("appsettings.local.json");
builder.AddAutofac(
    new ChatModule(),
    new DatabaseModule(builder.Configuration));
builder.ConfigureSettings<MongoSettings>(MongoSettings.Section);
builder.ConfigureSettings<GoogleAuthSettings>(GoogleAuthSettings.Section);
builder.ConfigureSettings<JwtSettings>(JwtSettings.Section);
builder.AddDefaultAllowAllCors();
builder.AddControllers();
builder.AddSwagger();
builder.AddSignalR();
builder.AddIdentity();

var app = builder.Build();
app.UseSwaggerDoc();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapSignalRHubs();

app.Run();


