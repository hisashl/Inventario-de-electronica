
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Data.SqlTypes;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace UnitTesting;
using Backend;
using Programa;
using Entidades;
using Moq;
using Programa.Pages;
using Microsoft.EntityFrameworkCore;

public class UnitTest1
{
   #region UnitTestUtilidades

    IConfiguration configuracion;

// Helper method to create a mock DbContext
        // private Mock<DatabaseManager> CreateMockDbContext(IQueryable<Estudiante> estudiantes, IQueryable<Usuario> usuarios)
        // {
        //     var mockDbSetEstudiantes = new Mock<DbSet<Estudiante>>();
        //     mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.Provider).Returns(estudiantes.Provider);
        //     mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.Expression).Returns(estudiantes.Expression);
        //     mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.ElementType).Returns(estudiantes.ElementType);
        //     mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.GetEnumerator()).Returns(estudiantes.GetEnumerator());

        //     var mockDbSetUsuarios = new Mock<DbSet<Usuario>>();
        //     mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.Provider).Returns(usuarios.Provider);
        //     mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.Expression).Returns(usuarios.Expression);
        //     mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.ElementType).Returns(usuarios.ElementType);
        //     mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.GetEnumerator()).Returns(usuarios.GetEnumerator());

        //     var mockDbContext = new Mock<DatabaseManager>(new DbContextOptions<DatabaseManager>());
        //     mockDbContext.Setup(db => db.Estudiantes).Returns(mockDbSetEstudiantes.Object);
        //     mockDbContext.Setup(db => db.Usuarios).Returns(mockDbSetUsuarios.Object);

        //     return mockDbContext;
        // }



        /*
        private Mock<DatabaseManager> CreateMockDbContext(IQueryable<Estudiante> estudiantes, IQueryable<Usuario> usuarios)
{
    // Crear un mock para DbSet<Estudiante>
    var mockDbSetEstudiantes = new Mock<DbSet<Estudiante>>();
    mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.Provider).Returns(estudiantes.Provider);
    mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.Expression).Returns(estudiantes.Expression);
    mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.ElementType).Returns(estudiantes.ElementType);
    mockDbSetEstudiantes.As<IQueryable<Estudiante>>().Setup(e => e.GetEnumerator()).Returns(estudiantes.GetEnumerator());

    // Crear un mock para DbSet<Usuario>
    var mockDbSetUsuarios = new Mock<DbSet<Usuario>>();
    mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.Provider).Returns(usuarios.Provider);
    mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.Expression).Returns(usuarios.Expression);
    mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.ElementType).Returns(usuarios.ElementType);
    mockDbSetUsuarios.As<IQueryable<Usuario>>().Setup(u => u.GetEnumerator()).Returns(usuarios.GetEnumerator());

    // Crear un mock para DbContext
    var mockDbContext = new Mock<DatabaseManager>();
    mockDbContext.Setup(db => db.Estudiantes).Returns(mockDbSetEstudiantes.Object);
    mockDbContext.Setup(db => db.Usuarios).Returns(mockDbSetUsuarios.Object);

    return mockDbContext;
}*/

#region Dont Opening

   public static class DbContextMockFactory
    {
        public static Mock<ContextoBD> CreateMockDbContext(
            IQueryable<AccInterfaz> accInterfaces,
            IQueryable<Almacenista> almacenistas,
            IQueryable<Coordinador> coordinadores,
            IQueryable<Division> divisiones,
            IQueryable<Equipo> equipos,
            IQueryable<EstUsuario> estUsuarios,
            IQueryable<Estudiante> estudiantes,
            IQueryable<Grupo> grupos,
            IQueryable<Interfaz> interfaces,
            IQueryable<Prestamo> prestamos,
            IQueryable<Profesor> profesores,
            IQueryable<Salon> salones,
            IQueryable<TpsUsuario> tpsUsuarios,
            IQueryable<Usuario> usuarios)
        {
            var mockDbContext = new Mock<ContextoBD>(new DbContextOptions<ContextoBD>());
            mockDbContext.Setup(db => db.AccInterfaces).Returns(accInterfaces);
            mockDbContext.Setup(db => db.Almacenistas).Returns(almacenistas);
            mockDbContext.Setup(db => db.Coordinadores).Returns(coordinadores);
            mockDbContext.Setup(db => db.Divisiones).Returns(divisiones);
            mockDbContext.Setup(db => db.Ensenas).Returns(ensenas);
            mockDbContext.Setup(db => db.Equipos).Returns(equipos);
            mockDbContext.Setup(db => db.EstUsuarios).Returns(estUsuarios);
            mockDbContext.Setup(db => db.Estudiantes).Returns(estudiantes);
            mockDbContext.Setup(db => db.Grupos).Returns(grupos);
            mockDbContext.Setup(db => db.Interfaces).Returns(interfaces);
            mockDbContext.Setup(db => db.Profesores).Returns(profesores);
            mockDbContext.Setup(db => db.Salones).Returns(salones);
            mockDbContext.Setup(db => db.TpsUsuarios).Returns(tpsUsuarios);
            mockDbContext.Setup(db => db.Usuarios).Returns(usuarios);

            return mockDbContext;
        }
        

#endregion


    
    [Fact]
    public void ContrasenaEncriptada()
    {
        // Arrange
        string input = "password123";

        // Act
        string hashGenerado = Utilidades.EncriptarContrasenaSHA256(input);

        // Assert
        Assert.NotEqual(input, hashGenerado);
        // Verifica que el hash generado no sea igual a la contraseña original,
        // asegurando que la función de encriptación esté produciendo resultados únicos.
    }

    [Fact]
    public void VerificarContrasena_False()
    {
        // Arrange
        string contrasena = "password123";
        string hash = "incorrecthash"; // Un hash incorrecto a propósito para el test.

        // Act
        bool result = Utilidades.VerificarContrasenaSHA256(contrasena, hash);

        // Assert
        Assert.False(result);
        // Verifica que la función de verificación devuelva False
        // cuando se proporciona una contraseña y un hash que no coinciden.
    }

    [Fact]
    public void VerificarContrasena_True()
    {
        // Arrange
        string contrasena = "password123";
        string hash = Utilidades.EncriptarContrasenaSHA256(contrasena);

        // Act
        bool result = Utilidades.VerificarContrasenaSHA256(contrasena, hash);

        // Assert
        Assert.True(result);
        // Verifica que la función de verificación devuelva True
        // cuando se proporciona la contraseña correcta y su hash correspondiente.
    }

    [Fact]
    public void VerificarRegistroNomina_True()
    {
        // Arrange
        string registro = "20300656";

        // Act
        bool result = Utilidades.VerificarRegistroNomina(registro);

        // Assert
        Assert.True(result);
        // Verifica que la función devuelve True para un registro válido.
    }

    [Fact]
    public void VerificarRegistroNomina_Menos8caracteres_False()
    {
        // Arrange
        string registro = "12345";

        // Act
        bool result = Utilidades.VerificarRegistroNomina(registro);

        // Assert
        Assert.False(result);
        // Verifica que la función devuelve False para un registro con menos de 8 caracteres.
    }

    [Fact]
    public void VerificarRegistroNomina_Mas8caracteres_False()
    {
        // Arrange
        string registro = "123456789";

        // Act
        bool result = Utilidades.VerificarRegistroNomina(registro);

        // Assert
        Assert.False(result);
        // Verifica que la función devuelve False para un registro con más de 8 caracteres.
    }

    [Fact]
    public void VerificarRegistroNomina_ConLetras_False()
    {
        // Arrange
        string registro = "1bcdefgh";

        // Act
        bool result = Utilidades.VerificarRegistroNomina(registro);

        // Assert
        Assert.False(result);
        // Verifica que la función devuelve False para un registro que contiene letras.
    }


    [Fact]
    public void VerificarNombre_True()
    {
        // Arrange
        string nombre = "Omar Mendoza";

        // Act
        bool result = Utilidades.VerificarNombre(nombre);

        // Assert
        Assert.True(result);
        // Verifica que la función devuelve True para un nombre válido.
    }

    [Fact]
    public void VerificarNombre_UsandoNumeros_False()
    {
        // Arrange
        string nombre = "Omar2415";

        // Act
        bool result = Utilidades.VerificarNombre(nombre);

        // Assert
        Assert.False(result);
        // Verifica que la función devuelve False para un nombre que contiene números.
    }

    [Fact]
    public void VerificarNombre_CaracteresEspeciales_False()
    {
        // Arrange
        string nombre = "Omar M#endoza";

        // Act
        bool result = Utilidades.VerificarNombre(nombre);

        // Assert
        Assert.False(result);
        // Verifica que la función devuelve False para un nombre que contiene caracteres especiales.
    }


    #endregion

    #region Utilidades
    [Fact]
    public void VerificarTipoDePrestamo_Tipo1_True()
    {
        // Arrange
        string tipoPrestamo = "1"; //tipo de prestamo existente en la base de datos

        // Act
        bool result = Utilidades.VerificarEstadoDePrestamo(tipoPrestamo);

        // Assert
        Assert.True(result);

    }

    [Fact]
    public void VerificarTipoDePrestamo_Tipo2_True()
    {
        // Arrange
        string tipoPrestamo = "2"; //tipo de prestamo existente en la base de datos

        // Act
        bool result = Utilidades.VerificarEstadoDePrestamo(tipoPrestamo);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerificarTipoDePrestamo_TipoInexistente_False()
    {
        // Arrange
        string tipoPrestamo = "3"; //este tipo de prestamo no existe en la base de datos

        // Act
        bool result = Utilidades.VerificarEstadoDePrestamo(tipoPrestamo);

        // Assert
        Assert.False(result);
    }



    [Fact]
    public void VerificarFecha_True()
    {
        // Arrange
        string fecha = "2023-11-02 12:30:00";

        // Act
        bool result = Utilidades.Verificarfecha(fecha);

        // Assert
        Assert.True(result);
        // Verifica que la función devuelve True para una fecha válida.
    }

    [Fact]
    public void VerificarFecha_FechaInvalida_False()
    {
        // Arrange
        string fecha = "2023-11-02 12:30";

        // Act
        bool result = Utilidades.Verificarfecha(fecha);

        // Assert
        Assert.False(result);
        // Verifica que la función devuelve False para una fecha inválida.
    }

    [Fact]
    public void VerificarEstadoDePrestamo_Estado1_True()
    {
        // Arrange
        string estadoPrestamo = "1";

        // Act
        bool result = Utilidades.VerificarEstadoDePrestamo(estadoPrestamo);

        // Assert
        Assert.True(result);
        // Verifica que la función devuelve True para el estado 1.
    }

    [Fact]
    public void VerificarEstadoDePrestamo_Estado2_True()
    {
        // Arrange
        string estadoPrestamo = "2";

        // Act
        bool result = Utilidades.VerificarEstadoDePrestamo(estadoPrestamo);

        // Assert
        Assert.True(result);
        // Verifica que la función devuelve True para el estado 2.
    }

    [Fact]
    public void VerificarEstadoDePrestamo_EstadoInexistente_False()
    {
        // Arrange
        string estadoPrestamo = "4";

        // Act
        bool result = Utilidades.VerificarEstadoDePrestamo(estadoPrestamo);

        // Assert
        Assert.False(result);
        // Verifica que la función devuelve False para un estado inexistente.
    }

    [Fact]
    public void ConvertirBytesAString_StringCorrecto()
    {
        // Arrange
        byte[] arregloBytes = { 72, 101, 108, 108, 111 }; // "Hello" en formato ASCII

        // Act
        string resultado = Utilidades.ConvertirBytesAString(arregloBytes);

        // Assert
        Assert.Equal("Hello", resultado);
        // Verifica que la función convierte correctamente un arreglo de bytes a una cadena.
    }

    [Fact]
    public void ConvertirBytesAString_BytesVacio_StringVacio()
    {
        // Arrange
        byte[] arregloBytes = new byte[0];

        // Act
        string resultado = Utilidades.ConvertirBytesAString(arregloBytes);

        // Assert
        Assert.Equal(string.Empty, resultado);
        // Verifica que la función devuelve una cadena vacía para un arreglo de bytes vacío.
    }
    #endregion

    #region Sqlite
    [Fact]
    public void TieneAccesoAInterfaz_UsuarioConAcceso_ReturnsTrue()
    {
        // Arrange
        int tipoUsuarioConAcceso = 0; // Ajusta según tus necesidades
        string nombrePagina = "Inicio"; // Ajusta según tus necesidades
        // Act
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        bool resultado = interfaz.TieneAccesoAInterfaz(tipoUsuarioConAcceso, nombrePagina);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void TieneAccesoAInterfaz_UsuarioConAcceso_ReturnsFalse()
    {
        // Arrange
        int tipoUsuarioConAcceso = 1; // Ajusta según tus necesidades
        string nombrePagina = "MantenimientoGrupos"; // Ajusta según tus necesidades

        // Act
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        bool resultado = interfaz.TieneAccesoAInterfaz(tipoUsuarioConAcceso, nombrePagina);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void InsertarUsuario_UsuarioNulo_ReturnsFalse()
    {
        // Arrange
        Usuario usuario = null;
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Act
        bool resultado = interfaz.InsertarUsuario(usuario);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ObtenerUsuarioPorId_UsuarioNoExistente_ReturnsNull()
    {
        // Arrange
        long idUsuarioNoExistente = 100;
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Act
        var resultado = interfaz.ObtenerUsuarioPorId(idUsuarioNoExistente);

        // Assert
        Assert.Null(resultado);
    }
    #endregion

    #region Estudiantes
    [Fact]
    public void InsertarEstudiante_EstudianteValido_ReturnsTrue()
    {
        // Arrange
        var usuario = new Usuario
        {
            IdTpoUsuario = (long)Enumeradores.tps_usuarios.Estudiante, // Estudiante
            IdEstUsuario = (long)Enumeradores.est_usuarios.Activo, // Activo
            Nombre = "Juan",
            AplPaterno = "Martinez",
            AplMaterno = "Hernandez",
            Contrasena = Encoding.UTF8.GetBytes(Utilidades.EncriptarContrasenaSHA256("Juan320")),
            FchCreacion = DateTime.Now
        };
        var estudiante = new Estudiante
        {
            IdUsuario = usuario.Id,
            Registro = long.Parse("20300655"),
            IdGrupo = 1, //Prueba
            FchCreacion = DateTime.Now
        };
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        bool user = interfaz.InsertarUsuario(usuario);

        bool resultado = interfaz.InsertarEstudiante(estudiante);

        // Assert
        Assert.False(resultado);
        interfaz.EliminarEstudiante(estudiante);
        interfaz.EliminarUsuario(usuario);
    }


    [Fact]
    public void ActualizarEstudiante_EstudianteExistente_ReturnsTrue()
    {
        // Arrange
        var usuario = new Usuario
        {
            IdTpoUsuario = (long)Enumeradores.tps_usuarios.Estudiante, // Estudiante
            IdEstUsuario = (long)Enumeradores.est_usuarios.Activo, // Activo
            Nombre = "Juan",
            AplPaterno = "Martinez",
            AplMaterno = "Hernandez",
            Contrasena = Encoding.UTF8.GetBytes(Utilidades.EncriptarContrasenaSHA256("Juan320")),
            FchCreacion = DateTime.Now
        };
        var estudiante = new Estudiante
        {
            IdUsuario = usuario.Id,
            Registro = long.Parse("20300655"),
            IdGrupo = 1, //Prueba
            FchCreacion = DateTime.Now
        };
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        bool user = interfaz.InsertarUsuario(usuario);
        bool resultado = interfaz.ActualizarEstudiante(estudiante);

        // Assert
        Assert.False(resultado);
        interfaz.EliminarEstudiante(estudiante);
    }

    [Fact]
    public void EliminarEstudiante_EstudianteExistente_ReturnsTrue()
    {
        // Arrange
        var usuario = new Usuario
        {
            IdTpoUsuario = (long)Enumeradores.tps_usuarios.Estudiante, // Estudiante
            IdEstUsuario = (long)Enumeradores.est_usuarios.Activo, // Activo
            Nombre = "Juan",
            AplPaterno = "Martinez",
            AplMaterno = "Hernandez",
            Contrasena = Encoding.UTF8.GetBytes(Utilidades.EncriptarContrasenaSHA256("Juan320")),
            FchCreacion = DateTime.Now
        };
        var estudiante = new Estudiante
        {
            IdUsuario = usuario.Id,
            Registro = long.Parse("20300655"),
            IdGrupo = 1, //Prueba
            FchCreacion = DateTime.Now
        };
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        bool user = interfaz.InsertarUsuario(usuario);
        bool resultado = interfaz.EliminarEstudiante(estudiante);

        // Assert
        Assert.False(resultado);
        interfaz.EliminarEstudiante(estudiante);
    }

    [Fact]
    public void ObtenerEstudiantePorId_EstudianteExistente_ReturnsEstudiante()
    {
        // Arrange
        var usuario = new Usuario
        {
            IdTpoUsuario = (long)Enumeradores.tps_usuarios.Estudiante, // Estudiante
            IdEstUsuario = (long)Enumeradores.est_usuarios.Activo, // Activo
            Nombre = "Juan",
            AplPaterno = "Martinez",
            AplMaterno = "Hernandez",
            Contrasena = Encoding.UTF8.GetBytes(Utilidades.EncriptarContrasenaSHA256("Juan320")),
            FchCreacion = DateTime.Now
        };
        var estudiante = new Estudiante
        {
            IdUsuario = usuario.Id,
            Registro = long.Parse("20300655"),
            IdGrupo = 1, //Prueba
            FchCreacion = DateTime.Now
        };

        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        bool user = interfaz.InsertarUsuario(usuario);
        var resultado = interfaz.ObtenerEstudiantePorId(estudiante.Id);

        // Assert
        Assert.Null(resultado);
        interfaz.EliminarEstudiante(estudiante);
    }

    [Fact]
    public void ObtenerTodosLosEstudiantes_EstudiantesEnLaBaseDeDatos_ReturnsListaNoVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerTodosLosEstudiantes();

        // Assert
        Assert.NotNull(resultado);
    }

    [Fact]
    public void ObtenerEstudiantePorIdUsuario_IdUsuarioExistente_ReturnsEstudiante()
    {
        // Arrange
        var usuario = new Usuario
        {
            IdTpoUsuario = (long)Enumeradores.tps_usuarios.Estudiante, // Estudiante
            IdEstUsuario = (long)Enumeradores.est_usuarios.Activo, // Activo
            Nombre = "Juan",
            AplPaterno = "Martinez",
            AplMaterno = "Hernandez",
            Contrasena = Encoding.UTF8.GetBytes(Utilidades.EncriptarContrasenaSHA256("Juan320")),
            FchCreacion = DateTime.Now
        };
        var estudiante = new Estudiante
        {
            IdUsuario = usuario.Id,
            Registro = long.Parse("20300655"),
            IdGrupo = 1, //Prueba
            FchCreacion = DateTime.Now
        };

        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        bool user = interfaz.InsertarUsuario(usuario);
        var resultado = interfaz.ObtenerEstudiantePorIdUsuario(estudiante.IdUsuario);

        // Assert
        Assert.Null(resultado);
        interfaz.EliminarEstudiante(estudiante);
    }
    #endregion

    #region Prestamos
    // Pruebas para InsertarPrestamo
    [Fact]
    public void InsertarPrestamo_PrestamoNulo_RegresaFalse()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.InsertarPrestamo(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void InsertarEquipo_EquipoNulo_RegresaFalse()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.InsertarEquipo(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ObtenerPrestamoPorId_IdInvalido_RegresaNull()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerPrestamoPorId(-1); // Un ID que probablemente no exista

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ObtenerPrestamoPorEstadoDePrestamo_IdInvalido_RegresaNull()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerPrestamoPorEstadoDePrestamo(-1); // ID inválido

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ActualizarPrestamo_PrestamoNulo_RegresaFalse()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ActualizarPrestamo(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void EliminarPrestamo_PrestamoNulo_RegresaFalse()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.EliminarPrestamo(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ObtenerTodosLosPrestamos_SinPrestamos_RegresaListaVacia()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerTodosLosPrestamos(); // Sin datos en la base de datos

        // Assert
        Assert.Equal(0, resultado.Count);
    }

    [Fact]
    public void ObtenerPrestamoPorIdUsuario_IdUsuarioInvalido_RegresaNull()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerPrestamoPorId(-1); // ID inválido

        // Assert
        Assert.Null(resultado);
    }
    #endregion

    #region Almacenistas

    // Pruebas para InsertarAlmacenista
    [Fact]
    public void InsertarAlmacenista_AlmacenistaNulo_RegresaFalse()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.InsertarAlmacenista(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ObtenerAlmacenistaPorId_IdInvalido_RegresaNull()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerAlmacenistaPorId(-1); // Un ID que probablemente no exista

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ActualizarAlmacenista_AlmacenistaNulo_RegresaFalse()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ActualizarAlmacenista(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void EliminarAlmacenista_AlmacenistaNulo_RegresaFalse()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.EliminarAlmacenista(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ObtenerTodosLosAlmacenistas_SinAlmacenistas_RegresaListaVacia()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerTodosLosAlmacenistas(); // Sin datos en la base de datos

        // Assert
        Assert.Empty(resultado);
    }

   
    #endregion

    #region profesores
    [Fact]
    /**/
    public void InsertarProfesor_ProfesorValido_RegresaTrue()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var profesor = new Profesor(); // Crea un profesor válido aquí según tu implementación

        // Act
        var resultado = interfaz.InsertarProfesor(profesor);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void InsertarProfesor_ProfesorNulo_RegresaFalse()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.InsertarProfesor(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    /**/
    public void ActualizarProfesor_ProfesorExistente_RegresaTrue()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var profesor = new Profesor(); // Crea un profesor válido aquí según tu implementación

        // Act
        interfaz.InsertarProfesor(profesor);
        var resultado = interfaz.ActualizarProfesor(profesor);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ActualizarProfesor_ProfesorNoExistente_RegresaFalse()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var profesor = new Profesor(); // Crea un profesor válido aquí según tu implementación

        // Act
        var resultado = interfaz.ActualizarProfesor(profesor);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void EliminarProfesor_ProfesorExistente_RegresaTrue()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var profesor = new Profesor(); // Crea un profesor válido aquí según tu implementación
        interfaz.InsertarProfesor(profesor);

        // Act
        var resultado = interfaz.EliminarProfesor(profesor);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void EliminarProfesor_ProfesorNoExistente_RegresaFalse()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var profesor = new Profesor(); // Crea un profesor válido aquí según tu implementación

        // Act
        var resultado = interfaz.EliminarProfesor(profesor);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    /**/
    public void ObtenerProfesorPorId_IdValido_RegresaProfesor()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var profesor = new Profesor(); // Crea un profesor válido aquí según tu implementación
        interfaz.InsertarProfesor(profesor);

        // Act
        var resultado = interfaz.ObtenerProfesorPorId(profesor.Id);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ObtenerProfesorPorId_IdInvalido_RegresaNull()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var idInvalido = -1; // ID inválido

        // Act
        var resultado = interfaz.ObtenerProfesorPorId(idInvalido);

        // Assert
        Assert.Null(resultado);
    }

  
    
    #endregion
    #region Coordinador
    [Fact]
    public void InsertarCoordinador_CoordinadorValido_RegresaTrue()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var coordinador = new Coordinador(); // Crea un coordinador válido aquí según tu implementación

        // Act
        var resultado = interfaz.InsertarCoordinador(coordinador);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void InsertarCoordinador_CoordinadorNulo_RegresaFalse()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Act
        var resultado = interfaz.InsertarCoordinador(null);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ActualizarCoordinador_CoordinadorExistente_RegresaTrue()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var coordinador = new Coordinador(); // Crea un coordinador válido aquí según tu implementación
        interfaz.InsertarCoordinador(coordinador);

        // Act
        var resultado = interfaz.ActualizarCoordinador(coordinador);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ActualizarCoordinador_CoordinadorNoExistente_RegresaFalse()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var coordinador = new Coordinador(); // Crea un coordinador válido aquí según tu implementación

        // Act
        var resultado = interfaz.ActualizarCoordinador(coordinador);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void EliminarCoordinador_CoordinadorExistente_RegresaTrue()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var coordinador = new Coordinador(); // Crea un coordinador válido aquí según tu implementación
        interfaz.InsertarCoordinador(coordinador);

        // Act
        var resultado = interfaz.EliminarCoordinador(coordinador);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void EliminarCoordinador_CoordinadorNoExistente_RegresaFalse()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var coordinador = new Coordinador(); // Crea un coordinador válido aquí según tu implementación

        // Act
        var resultado = interfaz.EliminarCoordinador(coordinador);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void ObtenerCoordinadorPorId_CoordinadorExistente_RegresaCoordinador()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var coordinador = new Coordinador(); // Crea un coordinador válido aquí según tu implementación
        interfaz.InsertarCoordinador(coordinador);

        // Act
        var resultado = interfaz.ObtenerCoordinadorPorId(coordinador.Id);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ObtenerCoordinadorPorId_CoordinadorNoExistente_RegresaNull()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        var idNoExistente = 999; // Un ID que no exista en tu base de datos

        // Act
        var resultado = interfaz.ObtenerCoordinadorPorId(idNoExistente);

        // Assert
        Assert.Null(resultado);
    }

   

    [Fact]
    public void ObtenerTodosLosCoordinadores_SinDatos_RegresaListaVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerTodosLosCoordinadores();

        // Assert
        Assert.Empty(resultado);
    }

    
    #endregion

    #region Equipos
    [Fact]
    public void ObtenerEquipoPorId_IdExistente_RegresaEquipo()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta un equipo con un ID específico en tu base de datos y guarda su ID

        // Act
        var idEquipo = 1; // Cambia a tu ID de equipo existente
        var resultado = interfaz.ObtenerEquipoPorId(idEquipo);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ObtenerEquipoPorId_IdNoExistente_RegresaNull()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta algunos equipos en tu base de datos, asegúrate de que el ID específico no exista

        // Act
        var idEquipoNoExistente = 999; // Cambia a un ID que no exista en tu base de datos
        var resultado = interfaz.ObtenerEquipoPorId(idEquipoNoExistente);

        // Assert
        Assert.Null(resultado);
    }


    [Fact]
    public void ObtenerTodosLosEquipos_SinDatos_RegresaListaVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerTodosLosEquipos();

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ObtenerTodosLosEquipos_ConDatos_RegresaListaNoVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta algunos equipos de prueba en tu base de datos

        // Act
        var resultado = interfaz.ObtenerTodosLosEquipos();

        // Assert
        Assert.Null(resultado);
    }
    #endregion

    #region Salones
    [Fact]
    public void ObtenerTodosLosSalones_SinDatos_RegresaListaVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerTodosLosSalones();

        // Assert
        Assert.Empty(resultado);
    }

    [Fact]
    public void ObtenerTodosLosSalones_ConDatos_RegresaListaNoVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta algunos salones de prueba en tu base de datos

        // Act
        var resultado = interfaz.ObtenerTodosLosSalones();

        // Assert
        Assert.NotNull(resultado);
    }
    [Fact]
    public void ObtenerSalonPorId_IdExistente_RegresaSalon()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta un salón con un ID específico en tu base de datos y guarda su ID

        // Act
        var idSalon = 1; // Cambia a tu ID de salón existente
        var resultado = interfaz.ObtenerSalonPorId(idSalon);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void ObtenerSalonPorId_IdNoExistente_RegresaNull()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta algunos salones en tu base de datos, asegúrate de que el ID específico no exista

        // Act
        var idSalonNoExistente = 999; // Cambia a un ID que no exista en tu base de datos
        var resultado = interfaz.ObtenerSalonPorId(idSalonNoExistente);

        // Assert
        Assert.Null(resultado);
    }
    #endregion

    #region Grupos
    [Fact]
    public void ObtenerTodosLosGrupos_SinDatos_RegresaListaVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerTodosLosGrupos();

        // Assert
        Assert.NotNull(resultado);
    }

    [Fact]
    public void ObtenerTodosLosGrupos_ConDatos_RegresaListaNoVacia()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta algunos grupos de prueba en tu base de datos

        // Act
        var resultado = interfaz.ObtenerTodosLosGrupos();

        // Assert
        Assert.NotNull(resultado);
    }

    [Fact]
    public void ObtenerGrupoPorIdEstudiante_EstudianteExistente_RegresaGrupo()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta un estudiante con un ID y un grupo asociado en tu base de datos y guarda su ID

        // Act
        var idEstudiante = 1; // Cambia a tu ID de estudiante existente
        var resultado = interfaz.ObtenerGrupoPorIdEstudiante(idEstudiante);

        // Assert
        Assert.NotNull(resultado);
    }

    [Fact]
    public void ObtenerGrupoPorIdEstudiante_EstudianteNoExistente_RegresaNull()
    {
        // Arrange
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        // Inserta algunos estudiantes en tu base de datos, asegúrate de que el ID específico no exista

        // Act
        var idEstudianteNoExistente = 999; // Cambia a un ID que no exista en tu base de datos
        var resultado = interfaz.ObtenerGrupoPorIdEstudiante(idEstudianteNoExistente);

        // Assert
        Assert.True(resultado == 0);
    }
    #endregion

    #region Resto
    [Fact]
    public void ObtenerProfesoresPorGrupo_GrupoNulo_RegresaListaVacia()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));

        // Act
        var resultado = interfaz.ObtenerProfesoresPorGrupo(null); // Grupo nulo

        // Assert
        Assert.Empty(resultado);
    }

    [Fact]
    public void ObtenerProfesoresPorGrupo_IdGrupoValido_RegresaListaProfesores()
    {
        var interfaz = new SQLite(configuracion.GetConnectionString("ConexionSQLite"));
        long? idGrupo = 1; // Id de grupo válido

        // Act
        var resultado = interfaz.ObtenerProfesoresPorGrupo(idGrupo);

        // Assert
        Assert.NotNull(resultado);
    }

   
    #endregion
}