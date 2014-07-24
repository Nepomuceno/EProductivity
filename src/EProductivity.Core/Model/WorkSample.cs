using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductivity.Core.Model
{
    public class WorkSample
    {
        public long WorkSampleId { get; set; }
        public List<Tour> Tours { get; set; }
        public List<Worker> Workers { get; set; }
        public Area Area { get; set; }
        public long AreaId { get; set; }
        public Organization Organization { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long OrganizationId { get; set; }
    }
}
