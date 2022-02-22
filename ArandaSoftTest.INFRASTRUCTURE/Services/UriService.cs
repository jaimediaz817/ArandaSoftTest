using ArandaSoftTest.CORE.QueryFilters;
using ArandaSoftTest.INFRASTRUCTURE.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArandaSoftTest.INFRASTRUCTURE.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseuri)
        {
            _baseUri = baseuri;
        }

        public Uri GetProductPaginationUri(ProductQueryFilter filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
