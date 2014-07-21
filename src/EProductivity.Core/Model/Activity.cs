using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Activity
    {
        public long ActivityId { get; set; }
        public string Name { get; set; }
        public bool BaseActivity { get; set; }
        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }
        public List<ActivityResponsability> ActivityResponsabilities { get; set; }
    }
}