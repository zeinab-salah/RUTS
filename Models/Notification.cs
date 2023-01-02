using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Models
{
    public class Notification : SharedColumns
    {
        [Required]
        public string Notification_body { get; set; }
    }
}
