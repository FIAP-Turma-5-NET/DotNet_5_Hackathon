using System.Data;
using System.Text;

using FIAP.HealthMed.API.Examples;

using FIAP_HealthMed.CrossCutting;
using FIAP_HealthMed.Domain.Enums;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using MySqlConnector;

using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

#region JWT
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


var secretJwt = configuration.GetValue<string>("SecretJWT");
if (string.IsNullOrEmpty(secretJwt))
{
    throw new InvalidOperationException("A chave 'SecretJWT' não foi encontrada ou está vazia no arquivo de configuração.");
}

var key = Encoding.ASCII.GetBytes(secretJwt);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#region SWAGGER
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FIAP Health Med",
        Version = "v1",
        Description = "API para gerenciamento de consultas médicas e usuários"
    });

    c.ExampleFilters();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
          {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                  },
                  Scheme = "oauth2",
                  Name = "Bearer",
                  In = ParameterLocation.Header,

              },
              new List<string>()
          }
      });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<UsuarioRequestExample>();
#endregion

//Configuração para buscar a connection
var connectionString = Environment.GetEnvironmentVariable("Connection_String");
builder.Services.AddScoped<IDbConnection>((connection) => new MySqlConnection(connectionString));

// Configuration IoC
builder.Services.AddRegisterCommonServices();
builder.Services.AddRegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
