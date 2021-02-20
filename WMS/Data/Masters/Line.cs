using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Data
{
    public class Line
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LineNumber { get; set; }

        [Required]
        public string LineName{ get; set; }


        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }


        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
        public int? FacilityId { get; set; }


        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }


        [ForeignKey("SubDepartmentId")]
        public SubDepartment SubDepartment { get; set; }
        public int? SubDepartmentId { get; set; }


        [ForeignKey("AreaId")]
        public DArea DArea { get; set; }
        public int? AreaId { get; set; }


        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public int RoomId { get; set; }
    }
}
