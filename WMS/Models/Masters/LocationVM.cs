using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Data;

namespace WMS.Models
{

    public class LocationVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Location Name*")]
        public string LocationName { get; set; }

        public CompanyVM Company { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }  //List can aslo be used
        [Required]
        [Display(Name = "Company Name*")]
        public int CompanyID { get; set; }


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
