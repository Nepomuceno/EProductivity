using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Area
    {
        public long AreaId { get; set; }
        public string Name { get; set; }
        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }
        public List<Function> Functions { get; set; }
    }
}