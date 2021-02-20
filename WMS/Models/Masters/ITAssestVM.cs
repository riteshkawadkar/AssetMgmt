using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WMS.Enums.ITAssets;

namespace WMS.Models
{
    public class ITAssestVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Asset Type*")]
        public AssetType AssetType { get; set; }

        [Display(Name = "Asset Make")]
        public string AssetMake { get; set; }

        [Display(Name = "Asset Model")]
        public string AssetModel { get; set; }

        [Display(Name = "OEM SL No")]
        public string OEMSlNo { get; set; }

        [Required]
        [Display(Name = "Host Name*")]
        public string HostName { get; set; }

        [Required]
        [Display(Name = "IP Address*")]
        public string IPAddress { get; set; }

        [Required]
        [Display(Name = "GMP Type*")]
        public GMPType GMPType { get; set; }

        [Display(Name = "OS Version")]
        public string OSVersion { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }


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

    }
}
