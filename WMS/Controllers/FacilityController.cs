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
    public class FacilityController : Controller
    {
        private readonly IFacilityRepository _repoFacility;
        private readonly ILocationRepository _repoLocation;
        private readonly IMapper _mapper;

        public FacilityController(IFacilityRepository repoFacility, ILocationRepository repoLocation, IMapper mapper)
        {
            _repoFacility = repoFacility;
            _repoLocation = repoLocation;
            _mapper = mapper;
        }

        // GET: Facility
        public async Task<ActionResult> Index()
        {
            var facilityList = await _repoFacility.FindAll(); 
            var model = _mapper.Map<IEnumerable<Facility>, IEnumerable<FacilityVM>>(facilityList);
            return View(model);
        }

        // GET: Facility/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoFacility.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var faciliity = await _repoFacility.FindById(id);
            var location = await _repoLocation.FindById(faciliity.LocationId);
            var locationModel = _mapper.Map<LocationVM>(location);

            var model = _mapper.Map<FacilityVM>(faciliity);
            model.Location = locationModel;

            return View(model);
        }

        // GET: Facility/Create
        public async Task<ActionResult> Create()
        {
            var locationList = await _repoLocation.FindAll(); //get list of companies
            // a way to covert companyList Company to SelectListItem
            var locationsItem = locationList.Select(q => new SelectListItem
            {
                Text = q.LocationName,
                Value = q.Id.ToString()
            });

            var model = new FacilityVM
            {
                Locations = locationsItem
            };
            return View(model);
        }

        // POST: Facility/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FacilityVM model)
        {
            try
            {
                // TODO: Add insert logic here
                var locationList = await _repoLocation.FindAll(); //get list of companies
                                                            // a way to covert companyList Company to SelectListItem
                var locationsItem = locationList.Select(q => new SelectListItem
                {
                    Text = q.LocationName,
                    Value = q.Id.ToString()
                });

                model.Locations = locationsItem;

                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var facilities = _mapper.Map<Facility>(model);

                var isSuccess = await _repoFacility.Create(facilities);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: Facility/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var isExists = await _repoFacility.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }
            var facility = await _repoFacility.FindById(id);
            var locationList = await _repoLocation.FindAll(); //get list of companies
            // a way to covert companyList Company to SelectListItem
            var locationsItem = locationList.Select(q => new SelectListItem
            {
                Text = q.LocationName,
                Value = q.Id.ToString()
            });

            FacilityVM model = new FacilityVM();
            model.Id = facility.Id;
            model.FacilityName = facility.FacilityName;
            model.Locations = locationsItem;
            model.LocationId = facility.LocationId;
            
            return View(model);
        }

        // POST: Facility/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FacilityVM model)
        {
            try
            {
                var locationList = await _repoLocation.FindAll(); 
                var locationsItem = locationList.Select(q => new SelectListItem
                {
                    Text = q.LocationName,
                    Value = q.Id.ToString()
                });

                model.Locations = locationsItem;

                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var facility = _mapper.Map<Facility>(model);

                var isSuccess = await _repoFacility.Update(facility);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: Facility/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var facility = await _repoFacility.FindById(id);

            if (facility == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoFacility.Delete(facility);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        


        
    }
}