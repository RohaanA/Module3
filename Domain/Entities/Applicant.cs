using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Applicant
    {
        [Key]
        public int ApplicantID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }

        public string? Resume { get; set; }
        [MaxLength(300)]
        public string? Skills { get; set; }
    }
}
