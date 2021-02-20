using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Data.Request;

namespace WMS.Models
{
    public class RequestTypeVM
    {
        public int Id { get; set; }

        public string RequestName { get; set; }
        public string Approvers { get; set; }

        public IEnumerable<string>  RoleApprovers { get; set; }

        

        public IEnumerable<RequestType> RequestNameList { get; set; }


        public IEnumerable<IdentityRole> RoleNameList { get; set; }

    }


    public class RequestTypeEditVM
    {
        public int Id { get; set; }

        public RequestTypeVM RequestName { get; set; }
        public string Approvers { get; set; }

        public IEnumerable<string> RoleApprovers { get; set; }

        public IEnumerable<IdentityRole> RoleNameList { get; set; }
    }
}
