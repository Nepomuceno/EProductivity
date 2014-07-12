using System;
using System.Collections.Generic;

namespace EProductivity.Core.Model
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string Document { get; set; }
        public OrganizationType Type { get; set; }
        public List<EProductivityUser> Users { get; set; }
        public List<WorkSample> WorkSamples { get; set; }
    }

    public enum OrganizationType
    {
        Business,
        Individual
    }
}