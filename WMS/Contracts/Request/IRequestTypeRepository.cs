using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Data.Request;

namespace WMS.Contracts
{
    public interface IRequestTypeRepository : IRepositoryBase<RequestType>
    {
    }
}
