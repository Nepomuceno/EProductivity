using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProductivity.Core.Model.Data.Convention
{
    public class KeyConvention : System.Data.Entity.ModelConfiguration.Conventions.Convention
    {
        public KeyConvention()
        {
            this.Properties()
                .Where(p => p.DeclaringType != null && p.Name == p.DeclaringType.Name + "Id")
                .Configure(p => p.IsKey());
        }
    }
}
