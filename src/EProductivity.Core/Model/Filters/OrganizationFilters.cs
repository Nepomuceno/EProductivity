using System.Linq;

namespace EProductivity.Core.Model.Filters
{
    public static class OrganizationFilters
    {
        public static IQueryable<Organization> Business(this IQueryable<Organization> organizations)
        {
            return organizations.Where(o => o.Type == OrganizationType.Business);
        }

        public static IQueryable<Organization> Individual(this IQueryable<Organization> organizations)
        {
            return organizations.Where(o => o.Type == OrganizationType.Individual);
        }

        public static IQueryable<Organization> WithDocument(this IQueryable<Organization> organizations,string document)
        {
            return organizations.Where(o => o.Document == document);
        }
    }
}