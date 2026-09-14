using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args); //constructor (objeto que se irá armando pieza por pieza)

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();//INYECCION DE DEPENDENCIAS, SE AGREGA EL SERVICIO DE OPENAPI
builder.Services.AddAuthorization();

builder.Services.AddCors( options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options=>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<Usuario, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(
                new System.Text.Json.Serialization.JsonStringEnumConverter());
        });;

var app = builder.Build(); //Aquí el builder ya terminó de armarse y se convierte en la aplicación real (app),
                           // lista para configurar cómo responde a peticiones.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())  // una tuberia por donde pasa cada petición
{
    app.MapOpenApi();// SOLO SE ACTIVA EL OPENAPI SI ESTAMOS EN DESARROLLO, NO EN PRODUCCION
}
app.UseExceptionHandler("/error");
app.UseCors("PermitirFrontend");
app.UseHttpsRedirection(); //fuerza a que toda petición use HTTPS (conexión segura)
app.UseAuthentication();
app.UseAuthorization(); //se asegura de que el usuario esté autorizado para acceder a un recurso
app.MapControllers(); 

app.Run();

