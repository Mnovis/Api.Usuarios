using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuração do CORS para dar permissão do Front-End Angular
builder.Services.AddCors(options => {

    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
//}

// Configurar a documentação do Scalar
app.MapScalarApiReference
    (options => options.WithTheme(ScalarTheme.BluePlanet));

// Habilita a política do CORS
app.UseCors("DefaultPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
