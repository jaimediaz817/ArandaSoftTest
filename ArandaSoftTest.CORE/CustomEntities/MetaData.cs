using System;
using System.Collections.Generic;
using System.Text;

namespace ArandaSoftTest.CORE.CustomEntities
{
    public class MetaData
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviusPage { get; set; }

        public int? NextPageNumber { get; set; }
        public int? PreviusPageNumber { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviusPageUrl { get; set; }
    }
}
