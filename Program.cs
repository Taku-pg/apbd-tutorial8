using apbd_tutorial8.Repository;
using apbd_tutorial8.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<ITrip_CountryRepository, Trip_CountryRepository>();
builder.Services.AddScoped<ITrip_CountryService, Trip_CountryService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClient_TripRepository, Client_TripRepository>();
builder.Services.AddScoped<IClient_TripService, Client_TripService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
