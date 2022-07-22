using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EgolePayUsersManagementSystem.Data
{
    public class FileUpload
    {
        public FileUpload()
        {
            CreatedAt = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public Roles Role { get; set; }
        public UpgradeRequestStatus Status { get; set; }
        public string DocumentPath1 { get; set; }
        public string DocumentPath2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ApplicationUser User { get; set; }

    }
}
