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
        public Tour Tour { get; set; }
        public long TourId { get; set; }
    }
}
