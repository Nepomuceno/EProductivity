using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductivity.Core.Model
{
    public class ActivityResponsability
    {
        public long ActivityResponsabilityId { get; set; }
        public Activity Activity { get; set; }
        public long ActivityId { get; set; }
        public Responsability Responsability { get; set; }
        public long ResponsabilityId { get; set; }
        public WorkType WorkType { get; set; }
    }

    public enum WorkType
    {
        Working = 1,
        Preparing = 2,
        NotWorking = 3
    }
}
