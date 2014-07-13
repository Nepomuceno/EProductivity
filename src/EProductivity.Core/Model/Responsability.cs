using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Responsability
    {
        public int ResponsabilityId { get; set; }
        public string Name { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public List<Worker> Workers { get; set; }
    }
}