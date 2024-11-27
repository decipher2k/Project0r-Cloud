using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer
{
    public class Calendar
    {
        public DateTime date { get; set; }
		public DateTime from { get; set; }
		public DateTime to { get; set; }
		public String text { get; set; }
		public String caption { get; set; }
		public bool handled { get; set; }
		public long Id { get; set; }
        

        public override string ToString()
        {
            return caption;
        }
    }
}
