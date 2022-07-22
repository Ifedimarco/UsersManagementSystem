using EgolePayUsersManagementSystem.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EgolePayUsersManagementSystem.Models
{
    public class CreateUpgradeRequest
    {
        [Required]
        public Roles Role { get; set; }
        [Required]
        public IFormFile DocumentPath1 { get; set; }
        [Required]
        public IFormFile DocumentPath2 { get; set; }
    }
}
