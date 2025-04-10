using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region SWAGGER
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FIAP Health Med",
        Version = "v1",
        Description = "API para gerenciamento de consultas médicas e usuários"
    });

    // Adiciona a definição do esquema de segurança (Bearer/JWT):
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey, // Use ApiKey para Bearer
        Scheme = "Bearer"
    });

    // Adiciona o requisito de segurança (dizendo que a API usa Bearer):
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
          {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer" // Este ID deve corresponder ao do AddSecurityDefinition
                  },
                  Scheme = "oauth2", //"Bearer",  // "oauth2" ou "Bearer"
                  Name = "Bearer",
                  In = ParameterLocation.Header,

              },
              new List<string>() // Lista vazia = aplica globalmente
          }
      });
});
#endregion


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
