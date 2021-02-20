using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data.Worfklow;

namespace WMS.Data.Workflow
{
    public class RequestWorkflow
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("WorkflowPasswordChangeId")]
        public WorkflowPasswordChange WorkflowPasswordChange { get; set; }
        public int WorkflowPasswordChangeId { get; set; }



        [ForeignKey("WorkflowGrantAccessId")]
        public WorkflowGrantAccess WorkflowGrantAccess { get; set; }
        public int WorkflowGrantAccessId { get; set; }
    }
}
