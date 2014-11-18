using System;
using System.Collections.Generic;

namespace EProductivity.Model
{
    public partial class Organization
    {
        public OrganizationType Type { get; set; }
        public string Document { get; set; }
    }
    public partial class Account
    {
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }
    }
    public enum OrganizationType
    {
        Individual,
        Business
    }

    public partial class WorkSample
    {
        public string Title { get; set; }
        public int WorkSampleId { get; set; }
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public List<Tour> Tours { get; set; }
        public List<Worker> Workers { get; set; }
        public WorkSampleStatus Status { get; set; }
    }

    public enum WorkSampleStatus
    {
        Open,
        Close
    }

    public partial class Tour
    {
        public int TourId { get; set; }
        public List<Observation> Observations { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public WorkSample WorkSample { get; set; }
        public int WorkSampleId { get; set; }
    }

    public partial class Observation
    {
        public int ObservationId { get; set; }
        public Worker Worker { get; set; }
        public int WorkerId { get; set; }
        public Activity Activity { get; set; }
        public int ActivityId { get; set; }
        public DateTime Date { get; set; }
        public Tour Tour { get; set; }
        public int TourId { get; set; }
    }

    public partial class Activity
    {
        public int ActivityId { get; set; }
        public string Description { get; set; }
        public ActivityType Type { get; set; }
    }

    public enum ActivityType
    {
        Work,
        Accessory,
        NotWork
    }
    public partial class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; }

    }

    public partial class Area
    {
        public string Name { get; set; }
        public int AreaId { get; set; }
    }
}
