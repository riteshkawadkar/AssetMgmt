using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Models.Worflow
{
    public class WorkflowGrantAccessVM : WorkflowBaseVM
    {
        public string Comment { get; set; }
    }
}
