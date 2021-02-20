using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Models.Worflow
{
    public class WorkflowBaseVM
    {
        public int Id { get; set; }

        public Location Location { get; set; }
        [Required]
        public int LocationId { get; set; }


        public Facility Facility { get; set; }
        public int? FacilityId { get; set; }


        public Department Department { get; set; }
        public int? DepartmentId { get; set; }


        public SubDepartment SubDepartment { get; set; }
        public int? SubDepartmentId { get; set; }


        public DArea DArea { get; set; }
        public int? AreaId { get; set; }


        public Room Room { get; set; }
        public int? RoomId { get; set; }



        public Equipment Equipment { get; set; }
        public int? EquipmentId { get; set; }


        /*        [ForeignKey("InstrumentId")]
                public Instrument Instrument { get; set; }
                public int? InstrumentId { get; set; }*/



        public ITAsset ITAsset { get; set; }
        public int? ITAssetId { get; set; }


        /*[ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public int? ApplicationId { get; set; }*/
    }
}
