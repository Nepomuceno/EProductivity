using EProductivity.Core.Model;
using System.Threading.Tasks;

namespace EProductivity.Core.Service
{
    public interface IOrganizationService
    {
        Task<Organization> RetrieveOrganizationAzync(string document, OrganizationType organizationType);
    }
}