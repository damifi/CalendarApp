using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApp.Models
{
    public class CalendarEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int Time { get; set; }

        public CalendarEntry()
        {

        }
    }
}
