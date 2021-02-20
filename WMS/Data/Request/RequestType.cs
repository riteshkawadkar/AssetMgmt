using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Data.Request
{
    public class RequestType
    {
        [Key]
        public int Id { get; set; }

        public string RequestName { get; set; }

        public string Approvers { get; set; }

    }
}
