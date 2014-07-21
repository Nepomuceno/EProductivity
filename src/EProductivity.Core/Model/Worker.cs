using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductivity.Core.Model
{
    public class Worker
    {
        public long WorkerId { get; set; }
        public string Name { get; set; }
        public Responsability Responsability { get; set; }
        public long ResponsabilityId { get; set; }
        public List<WorkSample> WorkSamples { get; set; }
        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }
        public List<Observation> Observations { get; set; }
    }
}
