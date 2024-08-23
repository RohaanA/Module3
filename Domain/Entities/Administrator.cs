using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Administrator
    {
        [Key]
        public int AdministratorID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
    }
}
