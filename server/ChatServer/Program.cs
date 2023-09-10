using ChatServer.Settings;
using ChatServer.Utils.AppRegistration;

var builder = WebApplication.CreateBuilder(args);
builder.AddAutofac();
builder.ConfigureSettings<MongoSettings>(MongoSettings.Section);
builder.AddDefaultAllowAllCors();
builder.AddControllers();
builder.AddSwagger();
builder.AddSignalR();

var app = builder.Build();
app.UseSwaggerDoc();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapSignalRHubs();

app.Run();


