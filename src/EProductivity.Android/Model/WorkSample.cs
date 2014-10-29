using System;
using System.Collections.Generic;
using Android.Hardware;

namespace EProductivity.Droid.Model
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public OrganizationType Type { get; set; }
        public string Document { get; set; }
    }
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public enum OrganizationType
    {
        Individual,
        Business
    }

    public class WorkSample
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

    public class Tour
    {
        public int TourId { get; set; }
        public List<Observation> Observations { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public WorkSample WorkSample { get; set; }
        public int WorkSampleId { get; set; }
    }

    public class Observation
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

    public class Activity
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
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; }

    }

    public class Area
    {
        public string Name { get; set; }
        public int AreaId { get; set; }
    }
}
