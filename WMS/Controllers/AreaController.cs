using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMS.Contracts;
using WMS.Data;
using WMS.Models;

namespace WMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AreaController : Controller
    {
        private readonly ILocationRepository _repoLocation;
        private readonly IFacilityRepository _repoFacility;
        private readonly IDepartmentRepository _repoDepartment;
        private readonly ISubDepartmentRepository _repoSubDepartment;
        private readonly IAreaRepository _repoArea;
        private readonly IMapper _mapper;

        public AreaController(ILocationRepository repoLocation, IFacilityRepository repoFacility,  IDepartmentRepository repoDepartment, ISubDepartmentRepository repoSubDepartment,
            IAreaRepository repoArea, IMapper mapper)
        {
            _repoLocation = repoLocation;
            _repoFacility = repoFacility;
            _repoDepartment = repoDepartment;
            _repoSubDepartment = repoSubDepartment;
            _repoArea = repoArea;
            _mapper = mapper;
        }

        // GET: Area
        public async Task<ActionResult> Index()
        {
            var areaList = await _repoArea.FindAll(); 
            var model = _mapper.Map<IEnumerable<DArea>, IEnumerable<AreaVM>>(areaList);
            return View(model);
        }

        // GET: Area/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoArea.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var area = await _repoArea.FindById(id);

            var location = await _repoLocation.FindById(area.LocationId);
            var locationModel = _mapper.Map<LocationVM>(location);

            var facility = await _repoFacility.FindById((int)area.FacilityId);
            var facilityModel = _mapper.Map<FacilityVM>(facility);

            var department = await _repoDepartment.FindById((int)area.DepartmentId);
            var deptModel = _mapper.Map<DepartmentVM>(department);

            var subdepartment = await _repoDepartment.FindById((int)area.SubDepartmentId);
            var subdeptModel = _mapper.Map<SubDepartmentVM>(subdepartment);

            var model = _mapper.Map<AreaVM>(area);
            model.Location = locationModel;
            model.Facility = facilityModel;
            model.Department = deptModel;
            model.SubDepartment = subdeptModel;

            return View(model);
        }

        // GET: Area/Create
        public async Task<ActionResult> AddEdit(int id = 0)
        {
            var locationList = await _repoLocation.FindAll(); //get list of companies
            // a way to covert companyList Company to SelectListItem
            var locationItem = locationList.Select(q => new SelectListItem
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


            var deptList = await _repoDepartment.FindAll(); //get list of companies
                                                        // a way to covert companyList Company to SelectListItem
            var deptItem = deptList.Select(q => new SelectListItem
            {
                Text = q.DepartmentName,
                Value = q.Id.ToString()
            });


            var subdeptList = await _repoSubDepartment.FindAll(); //get list of companies
                                                      // a way to covert companyList Company to SelectListItem
            var subdeptItem = subdeptList.Select(q => new SelectListItem
            {
                Text = q.SubDepartmentName,
                Value = q.Id.ToString()
            });

            var model = new AreaVM
            {
                Locations = locationItem,
                Facilities = facilityItem,
                Departmnets = deptItem,
                SubDepartmnets = subdeptItem
            };

            //CREATE
            if (id == 0)
            {
                return View(model);
            }
            //EDIT
            else
            {
                var isExists = await _repoArea.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var area = await _repoArea.FindById(id);

                model.Id = area.Id;
                model.AreaName = area.AreaName;
                model.LocationId = area.LocationId;

                if (model.FacilityId == 0)
                    model.FacilityId = null;
                else
                    model.FacilityId = area.FacilityId;

                if (model.DepartmentId == 0)
                    model.DepartmentId = null;
                else
                    model.DepartmentId = area.DepartmentId;

                if (model.SubDepartmentId == 0)
                    model.SubDepartmentId = null;
                else
                    model.SubDepartmentId = area.SubDepartmentId;
                return View(model);
            }
        }

        // POST: Area/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(int id, AreaVM model)
        {
            try
            {
                // TODO: Add insert logic here
                var locationList = await _repoLocation.FindAll(); //get list of companies
                                                            // a way to covert companyList Company to SelectListItem
                var locationItemList = locationList.Select(q => new SelectListItem
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


                var deptList = await _repoDepartment.FindAll(); //get list of companies
                                                          // a way to covert companyList Company to SelectListItem
                var deptItem = deptList.Select(q => new SelectListItem
                {
                    Text = q.DepartmentName,
                    Value = q.Id.ToString()
                });


                var subdeptList = await _repoSubDepartment.FindAll(); //get list of companies
                                                                // a way to covert companyList Company to SelectListItem
                var subdeptItem = subdeptList.Select(q => new SelectListItem
                {
                    Text = q.SubDepartmentName,
                    Value = q.Id.ToString()
                });

                model.Locations = locationItemList;
                model.Facilities = facilityItem;
                model.Departmnets = deptItem;
                model.SubDepartmnets = subdeptItem;

                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.FacilityId == 0)
                {
                    model.FacilityId = null;
                }
                if (model.DepartmentId == 0)
                {
                    model.DepartmentId = null;
                }
                if (model.SubDepartmentId == 0)
                {
                    model.SubDepartmentId = null;
                }

                var areas = _mapper.Map<DArea>(model);
                var isSuccess = false;

                if (id == 0)
                {
                    isSuccess = await _repoArea.Create(areas);
                }
                else
                {
                    isSuccess = await _repoArea.Update(areas);
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


        // GET: Area/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var area = await _repoArea.FindById(id);

            if (area == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoArea.Delete(area);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("api/afacility/{LocationId}")]
        public async Task<IEnumerable<Facility>> FacilityAsync(int LocationId)
        {
            return await _repoFacility.GetFacilitiesByLocation(LocationId);
        }

        [HttpGet("api/adepartment/{FacilityId}")]
        public async Task<IEnumerable<Department>> DepartmentAsync(int FacilityId)
        {
            return await _repoDepartment.GetDepartmentsByFacility(FacilityId);
        }

        [HttpGet("api/asubdepartment/{DepartmentId}")]
        public async Task<IEnumerable<SubDepartment>> SubDepartmentAsync(int DepartmentId)
        {
            return await _repoSubDepartment.GetSubDepartmentsByDept(DepartmentId);
        }
    }
}