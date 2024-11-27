﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectOrganizer
{
    [Serializable]
    public class Program
    {
        public String name {  get; set; }
        public String description { get; set; }
        public String executaleFile { get; set; }
        public bool startOnce { get; set; }

        [NonSerialized]
        public Process process;

        [NonSerialized]
        public ImageSource picture;
        public long Id { get; set; }

		public override String ToString()
        {
            return name;
        }
    }

}

