using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    public class Status
    {
        public int SId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
