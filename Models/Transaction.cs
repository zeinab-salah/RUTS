using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Models
{
    public class Transaction : SharedColumns
    {
        [Required]
        public string Amount { get; set; }
        public string? Narration { get; set; }
        [AllowNull]
        public DateTime? StatementDate { get; set; }
        public string Type { get; set; }
        public int? ResourcesItemId { get; set; }
        public virtual ResourcesItem ResourcesItem { get; set; }
        public int? BankId { get; set; }
        public virtual Bank Bank { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [Required]
        public int CorrespondentId { get; set; }
        [ForeignKey("CorrespondentId")]
        public Correspondent Correspondent { get; set; }
        public int? BenificaryId { get; set; }
        public virtual Benificary Benificary { get; set; }
        //foreign key to store the id of user who added the transaction
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
