
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
public class ListaPrestamosModel : PaginaBaseModel
{

    public List<TablaPrestamo> prestamos { get; set; }

    public int IdTpoUsuarioActual { get; set; }

    public int IdUsuarioActual { get; set; }

    public ListaPrestamosModel(IConfiguration configuracion = null) : base("ListaPrestamos", configuracion)
    {
        prestamos = new List<TablaPrestamo>();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        // Validacion
        if (HttpContext.Session.GetInt32("IdUsuarioActual") > 0)
        {
            if (HttpContext.Session.GetInt32("IdTpoUsuarioActual") == (int)Enumeradores.tps_usuarios.Coordinador)
            {
                Response.Redirect("/Login");
            }
        }
        else
        {
            Response.Redirect("/Index");
        }

        

        // Se declaran e inicializan todas aquellas variables necesarias para la consulta de la tabla
        prestamos = new List<TablaPrestamo>();
        List<PrmEquipo> prmEquipos = new List<PrmEquipo>();
        Profesor profesor = new Profesor();
        Estudiante estudiante = new Estudiante();
        Salon salon = new Salon();
        Prestamo prestamo = new Prestamo();
        Equipo equipo = new Equipo();
        Usuario usuario = new Usuario();
        string nombreUsuario = string.Empty;
        string estatus = string.Empty;
        string tipo = string.Empty;
        string frase = string.Empty;
    
        IdTpoUsuarioActual = HttpContext.Session.GetInt32("IdTpoUsuarioActual") == null ? 0 : (int)HttpContext.Session.GetInt32("IdTpoUsuarioActual");
        IdUsuarioActual = HttpContext.Session.GetInt32("IdUsuarioActual") == null ? 0 : (int)HttpContext.Session.GetInt32("IdUsuarioActual");


        try
        {
            // En caso de que el usuario sea un almacenista se le muestran todos los prestamos
            // De lo contrario solo se muestran los prestamos generados por el usuario Actual
            if (IdTpoUsuarioActual == (int)Enumeradores.tps_usuarios.Almacenista)
            {
                prmEquipos = SQLite.ObtenerTodosLosPrestamosDeEquipos();
            }
            else
            {
                prmEquipos = SQLite.ObtenerTodosLosPrestamosDeEquiposPorIdUsuario((long)IdUsuarioActual);
            }

            // Se cargan las filas con los registros correspondientes
            foreach (var prmEquipo in prmEquipos)
            {
                // Inicializar entidades
                try
                {
                    prestamo = SQLite.ObtenerPrestamoPorId(prmEquipo.IdPrestamo);
                    equipo = SQLite.ObtenerEquipoPorId(prmEquipo.IdEquipo);
                    salon = SQLite.ObtenerSalonPorId(prestamo.IdSalon);
                    estatus = Enum.GetName(typeof(Enumeradores.est_prestamos), prestamo.IdEstPrestamo);
                    if (prestamo.IdTpoPrestamo == (long)Enumeradores.tps_prestamos.Generado_por_un_profesor)
                    {
                        profesor = SQLite.ObtenerProfesorPorId(prestamo.IdProfesor);
                        usuario = SQLite.ObtenerUsuarioPorId(profesor.IdUsuario);
                        frase = "(Profesor)";
                        tipo = "Generado por un profesor";
                        nombreUsuario = string.Join(
                          " ",
                          usuario.Nombre,
                          usuario.AplPaterno,
                          usuario.AplMaterno,
                          frase
                        );
                    }
                    else
                    {
                        estudiante = SQLite.ObtenerEstudiantePorId(prestamo.IdEstudiante);
                        usuario = SQLite.ObtenerUsuarioPorId(estudiante.IdUsuario);
                        frase = "(Estudiante)";
                        tipo = "Generado por un estudiante";
                        nombreUsuario = string.Join(
                          " ",
                          usuario.Nombre,
                          usuario.AplPaterno,
                          usuario.AplMaterno,
                          frase
                        );
                    }
                    prestamos.Add(new TablaPrestamo
                    {
                        Id = (int)prmEquipo.Id,
                        Equipo = equipo.Nombre,
                        Cantidad = (int)prmEquipo.Cantidad,
                        Estatus = estatus,
                        Salon = salon.NmrSalon.ToString(),
                        GeneradoPor = nombreUsuario,
                        FechaInicio = prestamo.FchInicio,
                        FechaFin = prestamo.FchFin
                    });
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
public class TablaPrestamo
{
    public int Id { get; set; }
    public string Equipo { get; set; }
    public int Cantidad { get; set; }
    public string Estatus { get; set; }
    public string Salon { get; set; }
    public string GeneradoPor { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}


