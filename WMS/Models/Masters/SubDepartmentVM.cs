using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Models
{
    public class SubDepartmentVM
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Sub-Department Name*")]
        public string SubDepartmentName { get; set; }


        public DepartmentVM Department { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }  //List can aslo be used
        [Required]
        [DisplayName("Department Name*")]
        public int DepartmentId { get; set; }



    }
}
