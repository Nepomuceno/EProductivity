using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EProductivity.Core.Model;

namespace EProductivity.Web.Models
{
    public class Organization
    {
        public Guid OrganizationId { get; set; }
        public string Document { get; set; }
        public OrganizationType Type { get; set; }
        public List<EProductivityUser> Users { get; set; } 
    }

    public enum OrganizationType
    {
        Business,
        Individual
    }
}