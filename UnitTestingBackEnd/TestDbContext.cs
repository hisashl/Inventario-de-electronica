using Backend;
using Entidades;
using Microsoft.EntityFrameworkCore;

public class TestDbContext : DbContext
{
  public TestDbContext(DbContextOptions<TestDbContext> options): base(options)
  {
  }

  public DbSet<Almacenista> Almacenistas {get; set;} 
  // ...otros DbSet
}