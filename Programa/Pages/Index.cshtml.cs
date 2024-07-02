using Backend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Programa.Pages;

public class IndexModel : PaginaBaseModel
{
    public IndexModel(IConfiguration configuracion) : base("Index", configuracion) { }

    public void OnGet()
    {
        
        if (!(HttpContext.Session.GetInt32("IdUsuarioActual") > 0))
        {
            Response.Redirect("/Login");
        }
    }
}
