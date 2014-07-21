using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Responsability
    {
        public long ResponsabilityId { get; set; }
        public string Name { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }
        public List<Worker> Workers { get; set; }
        public List<ActivityResponsability> ActivityResponsabilities { get; set; }
    }
}