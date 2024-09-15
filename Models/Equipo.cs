using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFutbol.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    [Display(Name = "Nombre")]
    public string? NombreEquipo { get; set; }

    [Display(Name = "País")]
    public string? PaisEquipo { get; set; }

    [Display(Name = "Id")]
    public int? IdLiga { get; set; }

    [Display(Name = "Liga")]
    public virtual Liga? IdLigaNavigation { get; set; }

    public virtual ICollection<Jugador> Jugador { get; set; } = new List<Jugador>();
}
