using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductivity.Core.Model
{
    public class Tour
    {
        public long TourId { get; set; }
        public List<Observation> Observations { get; set; }
        public WorkSample WorkSample { get; set; }
        public long WorkSampleId { get; set; }
    }
}
