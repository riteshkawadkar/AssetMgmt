using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Data.Worfklow
{
    public class WorkflowBase
    {
        public string UserId { get; set; }
        public string UserName { get; set; }


        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        [Required]
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



        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
        public int? EquipmentId { get; set; }


/*        [ForeignKey("InstrumentId")]
        public Instrument Instrument { get; set; }
        public int? InstrumentId { get; set; }*/



        [ForeignKey("ITAssetId")]
        public ITAsset ITAsset { get; set; }
        public int? ITAssetId { get; set; }


        /*[ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public int? ApplicationId { get; set; }*/

    }
}
