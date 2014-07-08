using System;
using System.Threading.Tasks;

namespace EProductivity.Core.Model.Data
{
    public interface IModelContext
    {
        IModelCollection<Organization, Guid> Organizations { get; }
        IModelCollection<EProductivityUser, string> Users { get; }
        
        Task<int> SaveAsync();
    }
}
