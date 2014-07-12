using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductivity.Core.Model
{
    public class WorkSample
    {
        public long WorkSampleId { get; set; }
        public List<Tour> Tours { get; set; }
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
    }
}
