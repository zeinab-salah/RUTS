using System.ComponentModel.DataAnnotations;

namespace RUTS.Models
{
    public class Correspondent : SharedColumns
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
    }
}