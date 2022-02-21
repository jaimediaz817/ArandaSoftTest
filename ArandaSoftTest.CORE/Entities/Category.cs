using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ArandaSoftTest.CORE.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
