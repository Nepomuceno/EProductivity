namespace EProductivity.Core.Model
{
    public class Activity
    {
        public long ActivityId { get; set; }
        public string Name { get; set; }
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ActivityType ActivityType { get; set; }
        public long FunctionId { get; set; }
        public Function Function { get; set; }
    }
}