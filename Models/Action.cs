using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Models
{
    public class Action : SharedColumns
    {
        [Required]
        public string Action_Content { get; set; }
    }
}
