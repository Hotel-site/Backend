using eHotelMartinez.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

eHotelMartinez.DataAccess.DbSession.ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers( opptions => 
{
    opptions.Filters.Add<SessionAuthFilter>();
} );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();