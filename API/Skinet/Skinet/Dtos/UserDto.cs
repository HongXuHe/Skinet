using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Skinet.API.Dtos
{
    public class UserDto
    {
        public string  UserName { get; set; }
        [Required]
        public string  UserPassword { get; set; }
        [Required]
        public string UserEmail { get; set; }
    }
}
