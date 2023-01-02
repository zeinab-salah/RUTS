using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Models
{
    public class Currency : SharedColumns
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DollarExchangeRate { get; set; }
    }
}
