using System;
using System.ComponentModel.DataAnnotations;

namespace WMS.Models
{
    public class CompanyVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Company Name*")]
        public string CompanyName { get; set; }


        [Display(Name = "Corporate Office")]
        public string CorporateOffice { get; set; }

        [Required]
        [Display(Name = "Address Line1*")]
        public string AddressLine1 { get; set; }

        [Required]
        [Display(Name = "Address Line2*")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line3")]
        public string AddressLine3 { get; set; }

        [Display(Name = "Address Line4")]
        public string AddressLine4 { get; set; }

        [Display(Name = "Address Line5")]
        public string AddressLine5 { get; set; }

        [Required]
        [Display(Name = "Pin Code*")]
        public string PinCode { get; set; }

        [Required]
        [Display(Name = "State*")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City*")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Country*")]
        public string Country { get; set; }


    }

    

   
}
