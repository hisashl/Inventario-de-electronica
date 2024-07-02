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
public class MantenimientoPrestamosModel : PaginaBaseModel
{
    [BindProperty]
    public List<Salon> Salones { get; set; }

    [BindProperty]
    public DateTime FechaInicio { get; set; }

    [BindProperty]
    public DateTime FechaFin { get; set; }

    [BindProperty]
    public List<Equipo> EquiposDisponibles { get; set; }

    [BindProperty]
    public List<int> EquiposSeleccionados { get; set; }

    [BindProperty]
    public int CantidadEquipos { get; set; }

    public MantenimientoPrestamosModel(IConfiguration configuracion) : base ("MantenimientoPrestamos", configuracion) {}

    public void OnGet()
    {
        Salones = SQLite.ObtenerTodosLosSalones();
        EquiposDisponibles = SQLite.ObtenerTodosLosEquiposDisponibles();
    }

    public void OnPost()
    {
        // Lógica para manejar la solicitud POST
        // Puedes utilizar los valores de las propiedades para crear un nuevo préstamo
        // Por ejemplo: var nuevoPrestamo = new Prestamo { SalonId = ..., FechaInicio = ..., etc. };
    }
}