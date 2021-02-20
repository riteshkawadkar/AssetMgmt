using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.Data
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LocationName { get; set; }


        [ForeignKey("CompanyID")]
        public Company Company { get; set; }
        [Required]
        public int CompanyID { get; set; }


        [Required]
        public string AddressLine1 { get; set; }

        [Required]
        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string AddressLine5 { get; set; }

        [Required]
        public string PinCode { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

    }
}
