using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class PaginationExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new CustomException(HttpStatusCode.OK, "Page number cannot be less than 1.");

            if (pageSize < 1)
                throw new CustomException(HttpStatusCode.OK, "Page size cannot be less than 1.");

            int skip = (pageNumber - 1) * pageSize;
            return query.Skip(skip).Take(pageSize);
        }
    }
}
