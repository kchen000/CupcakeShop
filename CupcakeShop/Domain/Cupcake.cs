using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CupcakeShop.Domain
{
    public class Cupcake : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public string Ingredients { get; set; }
        public string ImageFileName { get; set; }
        public bool Featured { get; set; }
    }
}