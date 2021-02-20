using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data
{
    public class SubDepartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubDepartmentName { get; set; }


        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        [Required]
        public int DepartmentId { get; set; }

    }
}
