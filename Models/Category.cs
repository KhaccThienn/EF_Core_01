using System;
using System.Collections.Generic;

namespace EF_Core_01.Models;

public partial class Category
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public decimal? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
