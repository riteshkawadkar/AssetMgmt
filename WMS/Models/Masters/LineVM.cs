using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Models
{
    public class LineVM
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("line Number*")]
        public int LineNumber { get; set; }

        [Required]
        [DisplayName("Line Name*")]
        public string LineName { get; set; }


        public LocationVM Location { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }  //List can aslo be used
        [Required]
        [Display(Name = "Location Name*")]
        public int LocationId { get; set; }


        public FacilityVM Facility { get; set; }
        public IEnumerable<SelectListItem> Facilities { get; set; }  //List can aslo be used

        [Display(Name = "Facility Name")]
        public int? FacilityId { get; set; }


        public DepartmentVM Department { get; set; }
        public IEnumerable<SelectListItem> Departmnets { get; set; }  //List can aslo be used

        [Display(Name = "Department Name")]
        public int? DepartmentId { get; set; }


        public SubDepartmentVM SubDepartment { get; set; }
        public IEnumerable<SelectListItem> SubDepartmnets { get; set; }  //List can aslo be used

        [Display(Name = "Sub-Department Name")]
        public int? SubDepartmentId { get; set; }


        public AreaVM Area { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }  //List can aslo be used

        [Display(Name = "Area Name")]
        public int? AreaId { get; set; }


        public RoomVM Room { get; set; }
        public IEnumerable<SelectListItem> Rooms { get; set; }  //List can aslo be used
        [Required]
        [Display(Name = "Room Name")]
        public int RoomId { get; set; }
    }
}
