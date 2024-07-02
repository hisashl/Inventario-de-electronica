using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using Entidades;
using Backend;

namespace Programa.Pages;
public class Login : PaginaBaseModel
{
    // Inyecta la configuración de la aplicación en el constructor
    private readonly IConfiguration _configuration;

    // Propiedades para almacenar el ID del usuario y el tipo de usuario actual
    private int IdUsuarioActual { get; set; }
    private int IdTpoUsuarioActual { get; set; }

    // Constructor que recibe la configuración de la aplicación
    public Login(IConfiguration configuration) : base("Login", configuration)
    {
        _configuration = configuration;
    }

    // Método que se llama cuando se realiza una solicitud GET a la página de inicio de sesión
    // Si el usuario ya está autenticado, se redirige a la página principal
    public void OnGet()
    {
        HttpContext.Session.SetInt32("IdUsuarioActual", -1);
        HttpContext.Session.SetInt32("IdTpoUsuarioActual", -1);
        if (User.Identity.IsAuthenticated)
        {
            Response.Redirect("/Index");
        }
    }

    // Método que se llama cuando se realiza una solicitud POST a la página de inicio de sesión
    // Si el usuario es válido, se genera un token JWT y se almacena en la sesión
    // Si el usuario no es válido, se muestra un mensaje de error
    public async Task<IActionResult> OnPostAsync()
    {
        // Obtiene el registro y la contraseña del formulario de inicio de sesión
        string registro = Request.Form["Registro"];
        string contrasena = Request.Form["Contraseña"];

        // Busca al usuario en la base de datos
        bool usuarioValido = BuscarUsuario(registro, contrasena);

        if (usuarioValido)
        {
            // Genera un token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                 {
                new Claim("idUsuarioActual", IdUsuarioActual.ToString()),
                new Claim("idTpoUsuarioActual", IdTpoUsuarioActual.ToString())
              }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Almacena el token en la sesión
            HttpContext.Session.SetString("Token", tokenString);
            // Almacena IdUsuarioActual e IdTpoUsuarioActual en la sesión
            HttpContext.Session.SetInt32("IdUsuarioActual", IdUsuarioActual);
            HttpContext.Session.SetInt32("IdTpoUsuarioActual", IdTpoUsuarioActual);
            return RedirectToPage("/Index");
        }
        else
        {
            HttpContext.Session.SetInt32("IdUsuarioActual", -1);
            HttpContext.Session.SetInt32("IdTpoUsuarioActual", -1);
        }

        // Si el usuario no es válido, se muestra un mensaje de error
        ModelState.AddModelError("", "Error de inicio de sesión. Por favor, verifica tus credenciales.");
        return Page();
    }

    // Método que busca al usuario en la base de datos
    // Si el usuario es encontrado y la contraseña es correcta, devuelve true
    // Si el usuario no es encontrado o la contraseña es incorrecta, devuelve false
    public bool BuscarUsuario(string registro, string contrasena)
    {
        bool resultado = false;
        Usuario usuarioActual = null;

        try
        {
            // Intenta obtener al usuario de la base de datos
            usuarioActual = SQLite.ObtenerUsuarioPorRegistro(registro);
        }
        catch (Exception e)
        {
            // Si ocurre un error al obtener al usuario, se muestra un mensaje de error
            ModelState.AddModelError("", "Ocurrió un error al consultar el usuario, por favor inténtalo más tarde");
            return resultado;
        }

        if (usuarioActual == null)
        {
            // Si no se encuentra al usuario, se muestra un mensaje de error
            ModelState.AddModelError("", "No se encontró ningún usuario con ese registro.");
            return resultado;
        }

        // Si la contraseña es incorrecta, se muestra un mensaje de error
        if (Utilidades.EncriptarContrasenaSHA256(contrasena) != Utilidades.ConvertirBytesAString(usuarioActual.Contrasena))
        {
            ModelState.AddModelError("", "Las credenciales proporcionadas no son válidas.");
            return resultado;
        }

        // Si todo es correcto, se almacenan el ID del usuario y el tipo de usuario actual
        IdUsuarioActual = (int)usuarioActual.Id;
        IdTpoUsuarioActual = (int)usuarioActual.IdTpoUsuario;
        resultado = true;

        return resultado;
    }
}