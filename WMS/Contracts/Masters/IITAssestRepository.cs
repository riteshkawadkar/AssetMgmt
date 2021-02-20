using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface IITAssestRepository : IRepositoryBase<ITAsset>
    {
        Task<ICollection<ITAsset>> GetITAssetsByArea(int id);
        Task<bool> IsHostnameAvaiable(string hostname);
        Task<bool> IsIPAddressAvaiable(string ipaddress);
    }
}
