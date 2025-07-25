using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("PassResetTokens")]
    public class PassResetToken
    {
        public int Id { get; set; }
        
        [Required]
        [Column("UserID")]
        public int UserId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Token { get; set; }

        public bool IsUsed { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
