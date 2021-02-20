using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Data.Request
{
    public class MyRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RequestingEmployeeId")]
        public Employee RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }


        [ForeignKey("RequestTypeId")]
        public RequestType RequestType { get; set; }
        public int RequestTypeId { get; set; }


        public DateTime DateRequested { get; set; }

        public string RequestComments { get; set; }

        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }


        [ForeignKey("ApprovedById")]
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
