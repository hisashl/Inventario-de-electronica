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


using System.Text;

using Backend;
using Entidades;

namespace Programa;
public class Startup
{
    public IConfiguration Configuracion { get; }

    // Constructor que recibe la configuración de la aplicación
    public Startup(IConfiguration configuracion)
    {
        Configuracion = configuracion;
    }

    // Este método se llama por el tiempo de ejecución. Usa este método para agregar servicios al contenedor.
    public void ConfigureServices(IServiceCollection services)
    {
        // Agrega IConfiguration al contenedor de inyección de dependencias
        services.AddSingleton<IConfiguration>(Configuracion);

        // Configura el contexto de la base de datos
        services.AddDbContext<ContextoBD>(options =>
            options.UseSqlite(Configuracion.GetConnectionString("SQLite")));

        // Configura la autenticación JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuracion["Jwt:Issuer"],
                    ValidAudience = Configuracion["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuracion["Jwt:Key"]))
                };
            });

        // Agrega los controladores y las vistas
        services.AddControllersWithViews();
        services.AddRazorPages();

        // Establecer ruta de redirección a Login
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Login";
        });


        // Agrega el servicio de sesión
        services.AddSession();
    }

    // Este método se llama por el tiempo de ejecución. Usa este método para configurar el pipeline de solicitudes HTTP.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configura el entorno de desarrollo
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Configura el redireccionamiento a HTTPS
        app.UseHttpsRedirection();

        // Configura el uso de archivos estáticos
        app.UseStaticFiles();

        // Configura el enrutamiento
        app.UseRouting();

        // Configura la autenticación
        app.UseAuthentication();

        // Configura la autorización
        app.UseAuthorization();

        // Configura el middleware de sesión
        app.UseSession();

        // Configura los puntos finales
        // Endpoint para manejar rutas "/"
        app.UseEndpoints(endpoints =>
        {


            // Mapear controlador Login como default 
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}");

            // También se puede con Razor Page
            endpoints.MapRazorPages();
            endpoints.MapDefaultControllerRoute();

        });

    }
}
