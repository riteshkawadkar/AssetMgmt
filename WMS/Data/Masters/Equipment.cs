using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WMS.Enums.Equipments;

namespace WMS.Data
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        //public EType EquipmentType { get; set; }
        [Required]
        public string EquipmentName { get; set; }

        public string Equipmentmake { get; set; }

        public string EquipmentModel { get; set; }

        [Required]
        public int EquipmentNumber { get; set; }

        public DataStorage DataStorage { get; set; }

        public DirectIndirectImpact DirectIndirectImpact { get; set; }

        public QulaificationStatus QulaificationStatus { get; set; }

        public CSVStatus CSVStatus { get; set; }

        public Status Status { get; set; }


        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentType { get; set; }
        [Required]
        public int EquipmentTypeId { get; set; }


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
        public int? RoomId { get; set; }


        [ForeignKey("LineId")]
        public Line Line { get; set; }
        public int? LineId { get; set; }

    }
}
