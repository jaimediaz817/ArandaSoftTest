using ArandaSoftTest.CORE.QueryFilters;
using System;

namespace ArandaSoftTest.INFRASTRUCTURE.Interfaces
{
    public interface IUriService
    {
        Uri GetProductPaginationUri(ProductQueryFilter filters, string actionUrl);
    }
}