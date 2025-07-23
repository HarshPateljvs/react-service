using ApplicationSetup.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.BuildApp();

var app = builder.Build();
app.RunApp();
