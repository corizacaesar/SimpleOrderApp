﻿namespace ProductService.Models
{

    public partial class ProductData
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}
