using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Area
    {
        public int AreaId { get; set; }
        public string Name { get; set; }
        public List<Responsability> Responsabilities { get; set; }
    }
}