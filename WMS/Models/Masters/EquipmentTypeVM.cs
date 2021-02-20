using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Models
{
    public class EquipmentTypeVM
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Equipment Name*")]
        public string EquipmentName { get; set; }

        public IEnumerable<EquipmentType> EquipmentNameList { get; set; }

    }
}
