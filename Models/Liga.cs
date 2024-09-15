using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFutbol.Models;

public partial class Liga
{
    public int IdLiga { get; set; }

    [Display(Name = "Nombre")]
    public string? NombreLiga { get; set; }

    [Display(Name = "País")]
    public string? PaisLiga { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}
