using System;
using System.Threading.Tasks;

namespace EProductivity.Core.Model.Data
{
    public interface IModelContext
    {
        IModelCollection<Organization, int> Organizations { get; }
        IModelCollection<EProductivityUser, string> Users { get; }
        IModelCollection<WorkSample, long> WorkSamples { get; }
        IModelCollection<Tour, long> Tours { get; }
        IModelCollection<Observation, long> Observations { get; }
        IModelCollection<Worker, int> Workers { get; }
        IModelCollection<Area, int> Areas { get; }
        IModelCollection<Function, int> Functions { get; }
        IModelCollection<Activity, long> Activities { get; }
        Task<int> SaveAsync();
    }
}
