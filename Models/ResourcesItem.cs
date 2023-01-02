using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Models
{
    public class ResourcesItem : SharedColumns
    {
        [Required]
        public string Name { get; set; }
    }
}
