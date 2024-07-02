using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Programa.Pages;

namespace Programa.Controladores;
public class MenuModel : PaginaBaseModel
{
    public MenuModel(IConfiguration configuracion) : base("Menu", configuracion) { }

    public void OnGet() { }

    public IActionResult OnGetPartial()
    {
        return Partial("_MenuPartial", HttpContext.Session.GetInt32("IdTpoUsuarioActual"));
    }

}