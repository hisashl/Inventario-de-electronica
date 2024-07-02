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

public class ListaMantenimientosModel : PaginaBaseModel
{

    public List<TablaMantenimiento> _mantenimientos { get; set; }

    public int IdTpoUsuarioActual { get; set; }

    public int IdUsuarioActual { get; set; }

    public ListaMantenimientosModel(IConfiguration configuracion) : base("ListaMantenimientos", configuracion)
    {
        _mantenimientos = new List<TablaMantenimiento>();
    }
    

    public async Task<IActionResult> OnGetAsync()
    {
        // Validacion
        if (HttpContext.Session.GetInt32("IdUsuarioActual") > 0)
        {
            if (!(  HttpContext.Session.GetInt32("IdTpoUsuarioActual") == (int)Enumeradores.tps_usuarios.Coordinador |
                    HttpContext.Session.GetInt32("IdTpoUsuarioActual") == (int)Enumeradores.tps_usuarios.Almacenista))
            {
                Response.Redirect("/Login");
            }
        }
        else
        {
            Response.Redirect("/Index");
        }

        // Se declaran e inicializan todas aquellas variables necesarias para la consulta de la tabla
        _mantenimientos = new List<TablaMantenimiento>();
        List<Mantenimiento> mantenimientos = new List<Mantenimiento>();
        Equipo equipo = new Equipo();
        Almacenista almacenista = new Almacenista();
        Usuario usuario = new Usuario();
        string nombreUsuario = string.Empty;
        string tipo = string.Empty;
        string estatus = string.Empty;
        IdTpoUsuarioActual = HttpContext.Session.GetInt32("IdTpoUsuarioActual") == null ? 0 : (int)HttpContext.Session.GetInt32("IdTpoUsuarioActual");
        IdUsuarioActual = HttpContext.Session.GetInt32("IdUsuarioActual") == null ? 0 : (int)HttpContext.Session.GetInt32("IdUsuarioActual");

        try
        {
            // En caso de que el usuario sea un almacenista se le muestran todos los prestamos
            // De lo contrario solo se muestran los prestamos generados por el usuario Actual
            if(IdTpoUsuarioActual == (int)Enumeradores.tps_usuarios.Coordinador)
            {
                mantenimientos = SQLite.ObtenerTodosLosMantenimientos();
            }
            else
            {
                mantenimientos = SQLite.ObtenerTodosLosMantenimientosPorIdUsuario((long)IdUsuarioActual);
            }

            // Se cargan las filas con los registros correspondientes
            foreach (var mantenimiento in mantenimientos)
            {
                // Inicializar entidades
                try
                {
                    equipo = SQLite.ObtenerEquipoPorId(mantenimiento.IdEquipo);
                    almacenista = SQLite.ObtenerAlmacenistaPorId(mantenimiento.IdAlmacenista);
                    usuario = SQLite.ObtenerUsuarioPorId(almacenista.IdUsuario);
                    estatus  = Enum.GetName(typeof(Enumeradores.est_mantenimiento), mantenimiento.IdEstMantenimiento);
                    tipo  = Enum.GetName(typeof(Enumeradores.tps_mantenimiento), mantenimiento.IdTpoMantenimiento);
                    nombreUsuario = string.Join(
                        " ", 
                        usuario.Nombre, 
                        usuario.AplPaterno, 
                        usuario.AplMaterno
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ocurrio un error al consultar las entidades, porfavor intentalo mas tarde");
                    Console.WriteLine("Stack trace: " + e.StackTrace);
                    Console.WriteLine("Excepción datos: " + e.Data);
                    if (e.InnerException != null)
                    {
                        Console.WriteLine("Excepción interna: " + e.InnerException.Message);
                        Console.WriteLine("Excepción interna stack trace: " + e.InnerException.StackTrace);
                    }
                }

                _mantenimientos.Add(new TablaMantenimiento
                {
                    Id = (int) mantenimiento.Id,
                    Equipo = equipo.Nombre,
                    Descripcion = mantenimiento.Descripciones,
                    Refacciones = mantenimiento.Refaccion,
                    GeneradoPor = nombreUsuario,
                    Estatus = estatus,
                    Tipo = tipo
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocurrio un error al consultar los prestamos, porfavor intentalo mas tarde");
            Console.WriteLine("Stack trace: " + e.StackTrace);
            Console.WriteLine("Excepción datos: " + e.Data);
            if (e.InnerException != null)
            {
                Console.WriteLine("Excepción interna: " + e.InnerException.Message);
                Console.WriteLine("Excepción interna stack trace: " + e.InnerException.StackTrace);
            }
        }
        return Page();
    }

    public async Task OnPostEditarAsync(int id)
    {

    }

    public async Task OnPostEliminarAsync(int id)
    {

        
    }
}

// para mostrar la tabla con mayor facilidad
public class TablaMantenimiento
{
    public int Id { get; set; }
    public string Equipo { get; set; }
    public string Descripcion { get; set; }
    public string Refacciones { get; set; }
    public string GeneradoPor { get; set; }
    public string Estatus { get; set; }
    public string Tipo { get; set; }
}