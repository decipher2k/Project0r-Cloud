using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectOrganizer
{
    public class Log
    {
        public String caption { get; set; }
		public String description { get; set; }
		public DateTime date { get; set; }
		public String dateString { get { return date.ToShortDateString(); } }
        public String captionString { get { return caption; } }

        public long Id { get; set; }
		public override String ToString()
        {
            return caption;
        }
    }
}
