using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMS.Enums.Equipments;

namespace WMS.Models
{
    public class InstrumentVM
    {
        public int Id { get; set; }

        public InstrumentType Instrumentype { get; set; }

        [Required]
        [Display(Name = "Instrument Name*")]
        public string InstrumentName { get; set; }

        [Display(Name = "Instrument Make")]
        public string InstrumentMake { get; set; }

        [Display(Name = "Instrument Model")]
        public string InstrumentModel { get; set; }

        [Required]
        [Display(Name = "Instrument Asset Code*")]
        public int InstrumentAssetCode { get; set; }

        [Display(Name = "Data Storage")]
        public DataStorage DataStorage { get; set; }

        [Display(Name = "Direct/Indirect Impact")]
        public DirectIndirectImpact DirectIndirectImpact { get; set; }

        [Display(Name = "Qulaification Status")]
        public QulaificationStatus QulaificationStatus { get; set; }

        [Display(Name = "CSV Status")]
        public CSVStatus CSVStatus { get; set; }

        [Display(Name = "Computer Connected")]
        public ComputerConnected ComputerConnected { get; set; }

        [Display(Name = "Computer Asset Code")]
        public string ComputerAssetCode { get; set; }

        public Status Status { get; set; }


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

        [Display(Name = "Room Name")]
        public int? RoomId { get; set; }


        public LineVM Line { get; set; }
        public IEnumerable<SelectListItem> Lines { get; set; }  //List can aslo be used

        [Display(Name = "Line Name")]
        public int? LineId { get; set; }
    }
}
