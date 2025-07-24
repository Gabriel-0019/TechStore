using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("UserRoles")]
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("RoleID")]
        public required Role Role { get; set; }

        [ForeignKey("UserID")]
        public required User User { get; set; }
    }
}
