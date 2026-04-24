using eHotelMartinez.BusinessLogic.Core.Products;
using eHotelMartinez.BusinessLogic.Functions.Products;
using eHotelMartinez.BusinessLogic.Interfaces;


var builder = WebApplication.CreateBuilder(args);

eHotelMartinez.DataAccess.DbSession.ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IProductActions, ProductFlow>();

builder.Services.AddControllers();
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