using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Entities
{
   public class UserEntity:BaseEntity
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
    }
}
