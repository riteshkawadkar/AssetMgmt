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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repoDepartment;
        private readonly ILocationRepository _repoLocation;
        private readonly IFacilityRepository _repoFacility;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository repoDepartment, IFacilityRepository repoFacility, ILocationRepository repoLocation, IMapper mapper)
        {
            _repoDepartment = repoDepartment;
            _repoLocation = repoLocation;
            _repoFacility = repoFacility;
            _mapper = mapper;
        }

        // GET: Department
        public async Task<ActionResult> Index()
        {
            var deptList = await _repoDepartment.FindAll(); 
            var model = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentVM>>(deptList);
            return View(model);
        }

        // GET: Department/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoDepartment.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }
            var dept = await _repoDepartment.FindById(id);

            var location = await _repoLocation.FindById(dept.LocationId);
            var locationModel = _mapper.Map<LocationVM>(location);

            var facility = await _repoFacility.FindById((int)dept.FacilityId);
            var facilityModel = _mapper.Map<FacilityVM>(facility);

            var model = _mapper.Map<DepartmentVM>(dept);
            model.Location = locationModel;
            model.Facility = facilityModel;

            return View(model);
        }


        // GET: Department/Create
        public async Task<ActionResult> AddEdit(int id=0)
        {
            var locationList = await _repoLocation.FindAll(); //get list of companies
            // a way to covert companyList Company to SelectListItem
            var locationsItem = locationList.Select(q => new SelectListItem
            {
                Text = q.LocationName,
                Value = q.Id.ToString()
            });

            var facilityList = await _repoFacility.FindAll(); //get list of companies
                                                        // a way to covert companyList Company to SelectListItem
            var facilityItem = facilityList.Select(q => new SelectListItem
            {
                Text = q.FacilityName,
                Value = q.Id.ToString()
            });

            var model = new DepartmentVM
            {
                Locations = locationsItem,
                Facilities = facilityItem
            };


            //CREATE
            if (id == 0)
            {
                return View(model);
            }
            //EDIT
            else
            {
                var isExists = await _repoDepartment.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var dept = await _repoDepartment.FindById(id);
 
                model.Id = dept.Id;
                model.DepartmentName = dept.DepartmentName;
                model.LocationId = dept.LocationId;
                if (model.FacilityId == 0)
                {
                    model.FacilityId = null;
                }
                else
                {
                    model.FacilityId = dept.FacilityId;
                }

                return View(model);
            }
            
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(int id, DepartmentVM model)
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

                var facilityList = await _repoFacility.FindAll(); //get list of companies
                                                            // a way to covert companyList Company to SelectListItem                                      
                var facilityItem = facilityList.Select(q => new SelectListItem
                {
                    Text = q.FacilityName,
                    Value = q.Id.ToString()
                });

                model.Locations = locationsItem;
                model.Facilities = facilityItem;
                

                
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if(model.FacilityId == 0)
                {
                    model.FacilityId = null;
                }

                var depts = _mapper.Map<Department>(model);
                var isSuccess = false;

                if (id == 0)
                {
                    isSuccess = await _repoDepartment.Create(depts);
                }
                else
                {
                    isSuccess = await _repoDepartment.Update(depts);
                }

                

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

        
        // GET: Department/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var dept = await _repoDepartment.FindById(id);

            if (dept == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoDepartment.Delete(dept);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

       


        [HttpGet("api/facility/{LocationId}")]
        public async Task<IEnumerable<Facility>> FacilityAsync(int LocationId)
        {

            return await _repoFacility.GetFacilitiesByLocation(LocationId);

        }
    }
}