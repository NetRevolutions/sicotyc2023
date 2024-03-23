using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositoryLookupCodeExtensions
    {
        public static IQueryable<LookupCode> Search(this IQueryable<LookupCode> lookupCodes, string searchTerm) 
        {
            if (string.IsNullOrWhiteSpace(searchTerm))            
                return lookupCodes;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return lookupCodes.Where(lc => lc.LookupCodeName.ToLower().Contains(lowerCaseTerm)); 
            
        }

        public static IQueryable<LookupCode> Sort(this IQueryable<LookupCode> lookupCodes, string orderByQueryString)
        { 
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return lookupCodes.OrderBy(lc => lc.LookupCodeName);

            //var orderParams = orderByQueryString.Trim().Split(',');
            //var propertyInfos = typeof(LookupCode).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            //var orderQueryBuilder = new StringBuilder();

            //foreach (var param in orderParams)
            //{
            //    if (string.IsNullOrWhiteSpace(param))
            //        continue;

            //    var propertyFromQueryName = param.Split(" ")[0];
            //    var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            //    if (objectProperty == null)
            //        continue;

            //    var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            //    orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            //}
            //var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<LookupCode>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return lookupCodes.OrderBy(lc => lc.LookupCodeName);

            return lookupCodes.OrderBy(orderQuery);
        }

    }
}
