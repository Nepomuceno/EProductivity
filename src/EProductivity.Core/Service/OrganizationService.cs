using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EProductivity.Core.Model;
using EProductivity.Core.Model.Data;
using EProductivity.Core.Model.Filters;

namespace EProductivity.Core.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IModelContext _modelContext;

        public OrganizationService(IModelContext modelContext)
        {
            _modelContext = modelContext;
        }

        public async Task<Organization> RetrieveOrganizationAzync(string document, OrganizationType organizationType)
        {
            IQueryable<Organization> organizations = _modelContext.Organizations;
            if (organizationType == OrganizationType.Business)
                organizations = organizations.Business();
            else
                organizations = organizations.Individual();
            organizations.WithDocument(document);
            var organization = await organizations.FirstOrDefaultAsync();
            if (organization != null) return organization;
            organization = new Organization
            {
                Document = document,
                Type = organizationType
            };
            _modelContext.Organizations.Add(organization);
            await _modelContext.SaveAsync();
            return organization;
        }
    }
}