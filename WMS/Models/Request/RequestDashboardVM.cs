using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Models
{
    public class RequestDashboardVM
    {
        public int TotalRequests { get; set; }
        public int ApprovedRequests { get; set; }
        public int PendingRequests { get; set; }
        public int RejectedRequests { get; set; }

        public List<MyRequestVM> AppRequests { get; set; }
        
    }

    public class CreateAppRequestVM
    {

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }


        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        public RequestTypeVM RequestType { get; set; }
        public IEnumerable<SelectListItem> RequestTypes { get; set; }
        [Display(Name = "Request Type")]
        [Required]
        public int RequestTypeId { get; set; }


        [Display(Name = "Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
    }
}
