using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Area
    {
        public int AreaId { get; set; }
        public string Name { get; set; }
        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }
        public List<Responsability> Responsabilities { get; set; }
    }
}