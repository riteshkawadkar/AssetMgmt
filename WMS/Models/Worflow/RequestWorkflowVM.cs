using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Models.Worflow
{
    public class RequestWorkflowVM
    {
        public int Id { get; set; }


        public WorkflowPasswordChangeVM WorkflowPasswordChange { get; set; }
        public int WorkflowPasswordChangeId { get; set; }


        public WorkflowGrantAccessVM WorkflowGrantAccess { get; set; }
        public int WorkflowGrantAccessId { get; set; }
    }
}
