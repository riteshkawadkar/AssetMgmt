using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WMS.Data;

namespace WMS.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<Employee> Members { get; set; }
        public IEnumerable<Employee> NonMembers { get; set; }
    }
}
