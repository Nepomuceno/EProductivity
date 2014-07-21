using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductivity.Core.Model
{
    public class Observation
    {
        public long ObservationId { get; set; }
        public Worker Worker { get; set; }
        public long WorkerId { get; set; }
        public Responsability Responsability { get; set; }
        public long ResponsabilityId { get; set; }
        public Activity Activity { get; set; }
        public long ActivityId { get; set; }
        public Tour Tour { get; set; }
        public long TourId { get; set; }
        public DateTime Time { get; set; }
    }
}
