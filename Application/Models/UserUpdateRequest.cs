using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserUpdateRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public RolEnum Rol { get; set; }
    }
}
