using System;
using System.Collections.Generic;

namespace ProductService.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Stock { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
    }
}
