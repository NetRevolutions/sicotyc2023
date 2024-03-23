using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositoryLookupCodeGroupExtensions
    {
        public static IQueryable<LookupCodeGroup> Search(this IQueryable<LookupCodeGroup> lookupCodeGroups, string searchTerm) 
        { 
            if (string.IsNullOrWhiteSpace(searchTerm))
                return lookupCodeGroups;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return lookupCodeGroups.Where(lcg => lcg.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<LookupCodeGroup> Sort(this IQueryable<LookupCodeGroup> lookupCodeGroups, string orderByQueryString)
        { 
            if (string.IsNullOrEmpty(orderByQueryString))
                return lookupCodeGroups.OrderBy(lcg => lcg.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<LookupCodeGroup>(orderByQueryString);

            if (string.IsNullOrEmpty(orderQuery))
                return lookupCodeGroups.OrderBy(lcg => lcg.Name);

            return lookupCodeGroups.OrderBy(orderQuery);
        }
    }
}
