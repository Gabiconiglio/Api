﻿using System;
using System.Collections.Generic;

namespace Parcial_113917_Coniglio_Gabriel.Models;

public partial class Fabricante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Avione> Aviones { get; set; } = new List<Avione>();
}
