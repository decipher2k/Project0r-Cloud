using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer
{
    [Serializable]
    public class Data
    {
        private List<File> files = new List<File>();
        public List<File> Files
        {
            get
            {
                if (files == null)
                    files = new List<File>();
                return files;
            }
			set
			{
				files = value;
			}
		}
        
        private List<Program> apps = new List<Program>();
        public List<Program> Apps
        {
            get
            {
                if (apps == null)
                    apps = new List<Program>();
                return apps;
            }
			set
			{
				apps = value;
			}
		}


        private List<Note> notes = new List<Note>();
        public List<Note> Notes
        {
            get
            {
                if (notes == null)
                    notes = new List<Note>();
                return notes;
            }
	        set
	        {
		        notes = value;
	        }
        }

        private List<ToDo> todo = new List<ToDo>();
        public List<ToDo> ToDo
        {
            get
            {
                if (todo == null)
                    todo = new List<ToDo>();
                return todo;
            }
			set
			{
				todo = value;
			}
		}

        private List<Calendar> calendar = new List<Calendar>();
        public List<Calendar> Calendar
        {
            get
            {
                if (calendar == null)
                    calendar = new List<Calendar>();
                return calendar;
            }
            set
            {
                calendar = value;
            }
        }

        private List<Log> log = new List<Log>();
        public List<Log> Log
        {
            get
            {
                if (log == null)
                    log = new List<Log>();
                return log;
            }
            set
            {
                log=(List<Log>)value;
            }
        }

    }
}
