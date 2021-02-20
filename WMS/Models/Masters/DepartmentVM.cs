using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Department Name*")]
        public string DepartmentName { get; set; }


        public LocationVM Location { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }  //List can aslo be used
        [Required]
        [Display(Name = "Location Name*")]
        public int LocationId { get; set; }


        public FacilityVM Facility { get; set; }
        public IEnumerable<SelectListItem> Facilities { get; set; }  //List can aslo be used

        [Display(Name = "Facility Name")]
        public int? FacilityId { get; set; }
    }
}
