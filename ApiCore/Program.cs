var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Inicio de codigo ----- Codigo para evitar el problema de "as been blocked by CORS policy"
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});

// --- Fin de codigo //-----Codigo para evitar el problema de "as been blocked by CORS policy"


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// linea de codigo adicional para el problema de //-----Codigo para evitar el problema de "as been blocked by CORS policy"
app.UseCors();

app.MapControllers();

app.Run();
