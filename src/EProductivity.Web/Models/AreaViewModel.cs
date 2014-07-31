using System.Collections.Generic;
using EProductivity.Web.Controllers;

namespace EProductivity.Web.Models
{
    public class AreaViewModel
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public int TotalResponsabilities { get; set; }
        public IEnumerable<FunctionViewModel> Functions { get; set; }
    }
}