using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data
{
    public class Facility
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FacilityName { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        [Required]
        public int LocationId { get; set; }
    }
}
