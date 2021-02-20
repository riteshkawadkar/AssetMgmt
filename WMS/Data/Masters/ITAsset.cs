using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WMS.Enums.ITAssets;


namespace WMS.Data
{
    public class ITAsset
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public AssetType AssetType { get; set; }

        public string AssetMake { get; set; }

        public string AssetModel { get; set; }

        public string OEMSlNo { get; set; }

        [Required]
        public string HostName { get; set; }

        [Required]
        public string IPAddress { get; set; }

        [Required]
        public GMPType GMPType { get; set; }

        public string OSVersion { get; set; }

        public Status Status { get; set; }

        public string OwnerName { get; set; }

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
    }
}
