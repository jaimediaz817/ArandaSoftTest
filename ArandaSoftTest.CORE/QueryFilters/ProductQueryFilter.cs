using System;
using System.Collections.Generic;
using System.Text;

namespace ArandaSoftTest.CORE.QueryFilters
{
    public class ProductQueryFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        // Paginación
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
