using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Database
{
    public class DbLog
    {
        [Key]
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public DbLog()
        {

        }

        public DbLog(string msg)
        {
            Message = msg;
            Time = DateTime.Now;
        }

    }
}
