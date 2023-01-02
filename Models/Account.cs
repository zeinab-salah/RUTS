using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Models
{
    public class Account : SharedColumns
    {
        [Required]
        public string Ballance { get; set; }
        public string AccountNumber { get; set; }
        [Required]
        public int CorrespondentId { get; set; }
        public Correspondent Correspondent { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
    }
}
