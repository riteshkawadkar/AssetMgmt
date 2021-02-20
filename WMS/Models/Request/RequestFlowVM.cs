using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Models
{
    public class RequestFlowVM
    {
        public int Id { get; set; }


        public RequestTypeVM RequestType { get; set; }
        public IEnumerable<SelectListItem> RequestTypes { get; set; }  //List can aslo be used
        [Required]
        [Display(Name = "Request Type*")]
        public int RequestTypeId { get; set; }



    }
}
