using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Data
{
    public class EquipmentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EquipmentName { get; set; }
    }
}
