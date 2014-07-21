using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EProductivity.Web.Models
{
    public class Option
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class Category
    {
        public string text { get; set; }
        public IEnumerable<Option> children { get; set; }
    }

    public class DropdownOptions
    {
        public bool more { get; set; }
        public IEnumerable<Category> results { get; set; }
    }
}