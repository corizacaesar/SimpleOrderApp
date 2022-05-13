using System;
using System.Collections.Generic;

namespace OrderProcessor.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = null!;
        public string OrderContent { get; set; } = null!;
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
