using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        public int ProductModelID { get; set; }
        public string URLImage { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int ManufacturerModelID { get; set; }

        public virtual ManufacturerModel ManufacturerModels { get; set; }
    }
}