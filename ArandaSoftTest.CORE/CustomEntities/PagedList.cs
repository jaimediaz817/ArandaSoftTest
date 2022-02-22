using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArandaSoftTest.CORE.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount{ get; set; }

        public bool HasPreviusPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviusPageNumber => HasPreviusPage ? CurrentPage - 1 : (int?)null;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            // inicializar propiedades definidas
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            // obtener siempre el # superior: 8.1? 9,  8.6? 9
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Listado normal con propiedades adicionales
            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();

            // valor min: 1, omitir el # de page -1 * cantidad de pages:
            /* 
             10 reg por pag, skip recibe 1, 1-1=0;  
             */
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
