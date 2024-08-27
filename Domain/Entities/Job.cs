using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }
        public required string JobTitle { get; set; }
        public required string JobDescription { get; set; }
        [ForeignKey("Department")]
        public int DeptID { get; set; }
    }
}
