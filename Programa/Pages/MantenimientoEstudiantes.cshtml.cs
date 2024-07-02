using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Entidades;
using Backend;

namespace Programa.Pages;

public class MantenimientoEstudiantesModel : PaginaBaseModel
{

    public List<Grupo> Grupos { get; set; }
    public List<Estudiante> Estudiantes { get; set; }
    public List<Usuario> Usuarios { get; set; }



    public int IdTpoUsuarioActual { get; set;}

    public int IdUsuarioActual { get; set; }


    public MantenimientoEstudiantesModel(IConfiguration configuracion) : base("MantenimientoEstudiantes", configuracion)
    {

        Estudiantes = new List<Estudiante>();
        Usuarios = new List<Usuario>();

    }



    public async Task<IActionResult> OnGetAsync()
    {

        /*
        // Validacion
        if (HttpContext.Session.GetInt32("IdUsuarioActual") > 0)
        {
            if (!(HttpContext.Session.GetInt32("IdTpoUsuarioActual") == (int)Enumeradores.tps_usuarios.Coordinador |
                    HttpContext.Session.GetInt32("IdTpoUsuarioActual") == (int)Enumeradores.tps_usuarios.Almacenista))
            {
                Response.Redirect("/Login");
            }
        }
        else
        {
            Response.Redirect("/Index");
        }

        */

        Grupos = SQLite.ObtenerTodosLosGrupos();
        
        return Page();
    }

  

  /*
     public async Task<IActionResult> OnPostAsync()
    {

    
          try
        {

        // Crear una nueva instancia de Usuario

        UsuarioActual = new Usuario
            {
                IdTpoUsuario = (long)Enumeradores.tps_usuarios.Estudiante, // Estudiante
                IdEstUsuario = (long)Enumeradores.est_usuarios.Activo, // Activo
                Nombre = nuevoNombre,
                AplPaterno = nuevoAplPaterno,
                AplMaterno = nuevoAplMaterno,
                Contrasena = Encoding.UTF8.GetBytes(contrasenaEncriptada),
                FchCreacion = DateTime.Now
            };


            usuarioAgregado = SQLite.InsertarUsuario(UsuarioActual);

            if (usuarioAgregado)
            {
                // Crear una nueva instancia de Estudiante
                EstudianteActual = new Estudiante
                {
                    IdUsuario = UsuarioActual.Id,
                    Registro = long.Parse(nuevoRegistro),
                    IdGrupo = idGrupoSeleccionado, //Prueba
                    FchCreacion = DateTime.Now

                };


                // Guardar el nuevo usuario y estudiante en la base de datos
                estudianteAgregado = SQLite.InsertarEstudiante(EstudianteActual);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocurrió un error al agregar el nuevo estudiante, por favor inténtalo más tarde");
            Console.WriteLine($"Mensaje de error: {e.Message}");
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        if (!usuarioAgregado)
        {
            Console.WriteLine("Error al agregar el usuario. Verifica los datos y vuelve a intentarlo.");
            return;
        }

        if (!estudianteAgregado)
        {
            Console.WriteLine("Error al agregar el estudiante. Verifica los datos y vuelve a intentarlo.");
            return;
        }

        Console.WriteLine("Estudiante agregado exitosamente.");
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();

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


    */

    

}

