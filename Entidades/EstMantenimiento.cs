using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entidades;

[Table("est_mantenimiento")]
[Index("Id", IsUnique = true)]
public partial class EstMantenimiento
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [InverseProperty("IdEstMantenimientoNavigation")]
    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
