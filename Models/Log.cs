using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Models
{
    public class Log : SharedColumns
    {
        public string ActionId { get; set; }
        public Action Action { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
