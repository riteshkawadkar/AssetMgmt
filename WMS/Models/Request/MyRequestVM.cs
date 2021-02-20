using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Models
{
    public class MyRequestVM
    {
        public int Id { get; set; }


        public EmployeeVM RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }


        public RequestTypeVM RequestType { get; set; }
        public int RequestTypeId { get; set; }


        public DateTime DateRequested { get; set; }

        public string RequestComments { get; set; }

        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }


        public EmployeeVM ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
