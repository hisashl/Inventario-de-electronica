using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace UnitTestFrontEnd;

public class UnitTest1
{
    [Fact]
    public void Inicio_de_sesion_con_credenciales_incorrectas()
    {
        using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:5078/login");

                // Act
                IWebElement usernameField = driver.FindElement(By.Name("Registro"));
                usernameField.SendKeys("20300686");

                IWebElement passwordField = driver.FindElement(By.Name("Contraseña"));
                passwordField.SendKeys("Contrasena");

                IWebElement loginButton = driver.FindElement(By.Id("submit"));
                loginButton.Click();

                // Espera a que la redirección se complete (puedes usar diferentes estrategias, por ejemplo, esperar a que un elemento específico aparezca en la página siguiente)
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(drv => drv.Url.Contains("/login"));

                // Assert
                Assert.Contains("", driver.Url);
            }
    }
    [Fact]

    public void Inicio_de_sesion_con_credenciales_correctas(){

         using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:5078/login");

                // Act
                IWebElement usernameField = driver.FindElement(By.Name("Registro"));
                usernameField.SendKeys("12345678");

                IWebElement passwordField = driver.FindElement(By.Name("Contraseña"));
                passwordField.SendKeys("1");

                IWebElement loginButton = driver.FindElement(By.Id("submit"));
                loginButton.Click();

                // Espera a que la redirección se complete (puedes usar diferentes estrategias, por ejemplo, esperar a que un elemento específico aparezca en la página siguiente)
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(drv => drv.Url.Contains(""));

                // Assert
                Assert.Contains("", driver.Url);
            }
    }
     [Fact]     
    public void eliminar_registro_manteninmiento_con_credenciales_correctas(){

         using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:5078/login");

                // Act
                IWebElement usernameField = driver.FindElement(By.Name("Registro"));
                usernameField.SendKeys("12345678");

                IWebElement passwordField = driver.FindElement(By.Name("Contraseña"));
                passwordField.SendKeys("1");

                IWebElement loginButton = driver.FindElement(By.Id("submit"));
                loginButton.Click();
                 driver.Navigate().GoToUrl("http://localhost:5078/ListaMantenimientos");
                 driver.Navigate().GoToUrl("http://localhost:5078/ListaMantenimientos");

            // Encuentra todos los elementos de la columna de botones de eliminación
            var deleteButtons = driver.FindElements(By.CssSelector(".btn-danger"));

            // Verifica si hay al menos un botón de eliminación
            if (deleteButtons.Any())
            {
                // Hace clic en el último botón de eliminación
                var lastDeleteButton = deleteButtons.Last();
                lastDeleteButton.Click();
            }
            else
            {
                Console.WriteLine("No hay registros para eliminar en la tabla.");
            }
            }
    }
    [Fact]
     public void eliminar_prestamo_manteninmiento_con_credenciales_correctas(){

         using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:5078/login");

                // Act
                IWebElement usernameField = driver.FindElement(By.Name("Registro"));
                usernameField.SendKeys("12345678");

                IWebElement passwordField = driver.FindElement(By.Name("Contraseña"));
                passwordField.SendKeys("1");

                IWebElement loginButton = driver.FindElement(By.Id("submit"));
                loginButton.Click();
                 driver.Navigate().GoToUrl("http://localhost:5078/ListaPrestamos");
                 driver.Navigate().GoToUrl("http://localhost:5078/ListaPrestamos");

            // Encuentra todos los elementos de la columna de botones de eliminación
            var deleteButtons = driver.FindElements(By.CssSelector(".btn-danger"));

            // Verifica si hay al menos un botón de eliminación
            if (deleteButtons.Any())
            {
                // Hace clic en el último botón de eliminación
                var lastDeleteButton = deleteButtons.Last();
                lastDeleteButton.Click();
            }
            else
            {
                Console.WriteLine("No hay registros para eliminar en la tabla.");
            }
            }
    }

}