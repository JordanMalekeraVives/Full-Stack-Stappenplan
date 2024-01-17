using System;
using System.Collections.Generic;

namespace FullStackStappenplan.Domains.Entities;

public partial class Beer
{
    public int Biernr { get; set; }

    public string Naam { get; set; } = null!;

    public int Brouwernr { get; set; }

    public int Soortnr { get; set; }

    public decimal? Alcohol { get; set; }

    public string? Image { get; set; }

    public virtual Brewery BrouwernrNavigation { get; set; } = null!;

    public virtual Variety SoortnrNavigation { get; set; } = null!;
}
