using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMS.Contracts;
using WMS.Data;
using WMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace WMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EquipmentTypeController : Controller
    {
        private readonly IEquipmentTypeRepository _repoEquipmentType;
        private readonly IMapper _mapper;

        public EquipmentTypeController(IEquipmentTypeRepository repoEquipmentType, IMapper mapper)
        {
            _repoEquipmentType = repoEquipmentType;
            _mapper = mapper;
        }

        // GET: EquipmentTypeController/Create
        public async Task<ActionResult> Index()
        {
            var etList = await _repoEquipmentType.FindAll();
            var model = _mapper.Map<IEnumerable<EquipmentType>, IEnumerable<EquipmentTypeVM>>(etList);

            var modelET = new EquipmentTypeVM()
            {
                EquipmentNameList = etList
            };

            return View(modelET);
        }

        // POST: EquipmentTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EquipmentTypeVM model)
        {
            try
            {
                var et = _mapper.Map<EquipmentType>(model);
                var isSuccess = false;

                isSuccess = await _repoEquipmentType.Create(et);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       

        // GET: EquipmentTypeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var et = await _repoEquipmentType.FindById(id);

            if (et == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoEquipmentType.Delete(et);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        
    }
}
