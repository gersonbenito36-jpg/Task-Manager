using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<Usuario> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("registro")] //Endpoint para el registro
    public async Task<IActionResult>Registro([FromBody] RegistroDTO dto)
    {
        var usuario = new Usuario // creamos un objeto usuario
        {
            UserName = dto.Email, 
            Email = dto.Email
        };
        var resultado = await _userManager.CreateAsync(usuario, dto.Password); // creamos la contraseña a parte en una variable
        if(!resultado.Succeeded)  // si la contraseña no cumple con algunos requisitos, será rechazada con un error
        {
            return BadRequest(resultado.Errors);
        }
        return Ok ("Usuario registrado exitosamente");
    }
    [HttpPost("login")]
    public async Task<IActionResult>Login ([FromBody] LoginDTO dto)
    {
        var usuario = await _userManager.FindByEmailAsync(dto.Email); // buscamos un usuario con ese email
        if(usuario == null)  // si no encuentra nadie con ese correo
        {
            return Unauthorized("Email o contraseña incorrecto"); // este error será si no encuentra nadie. 
        }
        var contraseñaValida = await _userManager.CheckPasswordAsync(usuario, dto.Password); //checkamos que la contreña coincida
        if(!contraseñaValida)  //si la contraseña es diferente a la guardada, lanzará un error
        {
            return Unauthorized("Contraseña o Email son incorrectos");
        }

        var claims = new List<Claim> // creamos un objeto , que nos permite crear el token, con los datos que queremos que tenga el token, en este caso el id y el email del usuario
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email)
        };
        var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));// volvemos a solicitar la llave secreta 
        var tusCredenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
        var tuToken = new JwtSecurityToken(
            claims : claims,
            expires : DateTime.UtcNow.AddHours(2), // tiempo expiracion en horas
            signingCredentials : tusCredenciales
        );
            
        
        var TokenString = new JwtSecurityTokenHandler().WriteToken(tuToken);
        return Ok(new { token = TokenString}); // nuevo objeto en como aparecerá el token en el json
    }
}