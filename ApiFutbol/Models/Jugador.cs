using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFutbol.Models;

public partial class Jugador
{
    public int IdJugador { get; set; }

    [Display(Name = "Nombre")]
    public string NombreJugador { get; set; } = null!;

    [Display(Name = "País")]
    public string PaisJugador { get; set; } = null!;

    [Display(Name = "Posición")]
    public int IdPosicion { get; set; }

    [Display(Name = "Equipo")]
    public int IdEquipo { get; set; }

    [Display(Name = "Fecha de Nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    public string? Imagen { get; set; }

    [Display(Name = "Equipo")]
    public virtual Equipo? IdEquipoNavigation { get; set; }

    [Display(Name = "Posición")]
    public virtual Posicion? IdPosicionNavigation { get; set; } 
}
