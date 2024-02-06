﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int ProductBrandId { get; set; }        // Foreign Key : Not Allow Null
        public ProductBrand ProductBrand { get; set; } // Navigational Property [One]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
