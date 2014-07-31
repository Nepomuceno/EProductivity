using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Function
    {
        public long FunctionId { get; set; }
        public string Name { get; set; }
        public Area Area { get; set; }
        public long AreaId { get; set; }
        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }
        public List<Worker> Workers { get; set; }
        public List<Activity> Activities { get; set; }
    }
}