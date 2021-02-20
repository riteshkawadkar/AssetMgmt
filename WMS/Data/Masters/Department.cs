using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Data
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        [Required]
        public int LocationId { get; set; }


        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
        public int? FacilityId { get; set; }
    }
}
