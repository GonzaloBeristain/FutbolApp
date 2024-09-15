using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFutbol.Models;

public partial class Posicion
{
    public int IdPosicion { get; set; }

    [Display(Name = "Nombre")]
    public string? NombrePosicion { get; set; }

    public virtual ICollection<Jugador> Jugador { get; set; } = new List<Jugador>();
}
