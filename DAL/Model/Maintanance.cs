using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Maintanance
    {
        public int Id { get; set; }
        public DateTime LastMaintanance { get; set; }
        public bool MaintananceRequired { get; set; }
    }
}
