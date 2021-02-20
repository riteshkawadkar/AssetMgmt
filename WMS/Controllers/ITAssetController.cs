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
    public class ITAssetController : Controller
    {
        private readonly ILocationRepository _repoLocation;
        private readonly IFacilityRepository _repoFacility;
        private readonly IDepartmentRepository _repoDepartment;
        private readonly ISubDepartmentRepository _repoSubDepartment;
        private readonly IAreaRepository _repoArea;
        private readonly IITAssestRepository _repoAsset;
        private readonly IMapper _mapper;

        public ITAssetController(ILocationRepository repoLocation, IFacilityRepository repoFacility, IDepartmentRepository repoDepartment, ISubDepartmentRepository repoSubDepartment,
         IAreaRepository repoArea, IITAssestRepository repoAsset, IMapper mapper)
        {
            _repoLocation = repoLocation;
            _repoFacility = repoFacility;
            _repoDepartment = repoDepartment;
            _repoSubDepartment = repoSubDepartment;
            _repoArea = repoArea;
            _repoAsset = repoAsset;
            _mapper = mapper;
        }

        // GET: ITAssetController
        public async Task<ActionResult> Index()
        {
            var assetList = await _repoAsset.FindAll();
            var model = _mapper.Map<IEnumerable<ITAsset>, IEnumerable<ITAssestVM>>(assetList);
            return View(model);
        }

        // GET: ITAssetController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoAsset.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var asset = await _repoAsset.FindById(id);

            var location = _repoLocation.FindById(asset.LocationId);
            var locationModel = _mapper.Map<LocationVM>(location);

            var facility = _repoFacility.FindById((int)asset.FacilityId);
            var facilityModel = _mapper.Map<FacilityVM>(facility);

            var department = _repoDepartment.FindById((int)asset.DepartmentId);
            var deptModel = _mapper.Map<DepartmentVM>(department);

            var subdepartment = _repoDepartment.FindById((int)asset.SubDepartmentId);
            var subdeptModel = _mapper.Map<SubDepartmentVM>(subdepartment);

            var darea = _repoArea.FindById((int)asset.AreaId);
            var areaModel = _mapper.Map<AreaVM>(darea);

            var model = _mapper.Map<ITAssestVM>(asset);
            model.Location = locationModel;
            model.Facility = facilityModel;
            model.Department = deptModel;
            model.SubDepartment = subdeptModel;
            model.Area = areaModel;


            return View(model);
        }

        // GET: ITAssetController/Create
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

            var areaList = await _repoArea.FindAll(); //get list of companies
                                                // a way to covert companyList Company to SelectListItem
            var areaItem = areaList.Select(q => new SelectListItem
            {
                Text = q.AreaName,
                Value = q.Id.ToString()
            });



            var model = new ITAssestVM
            {
                Locations = locationItem,
                Facilities = facilityItem,
                Departmnets = deptItem,
                SubDepartmnets = subdeptItem,
                Areas = areaItem,

            };


            //CREATE
            if (id == 0)
            {
                return View(model);
            }
            //EDIT
            else
            {
                var isExists = await _repoAsset.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var asset = await _repoAsset.FindById(id);

                model.Id = asset.Id;
                model.AssetMake = asset.AssetMake;
                model.LocationId = asset.LocationId;

                if (model.FacilityId == 0)
                    model.FacilityId = null;
                else
                    model.FacilityId = asset.FacilityId;

                if (model.DepartmentId == 0)
                    model.DepartmentId = null;
                else
                    model.DepartmentId = asset.DepartmentId;

                if (model.SubDepartmentId == 0)
                    model.SubDepartmentId = null;
                else
                    model.SubDepartmentId = asset.SubDepartmentId;

                if (model.AreaId == 0)
                    model.AreaId = null;
                else
                    model.AreaId = asset.AreaId;


                return View(model);
            }
        }

        // POST: ITAssetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(int id, ITAssestVM model)
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


                var areaList = await _repoArea.FindAll(); //get list of companies
                                                    // a way to covert companyList Company to SelectListItem
                var areaItem = areaList.Select(q => new SelectListItem
                {
                    Text = q.AreaName,
                    Value = q.Id.ToString()
                });


                

                model.Locations = locationItemList;
                model.Facilities = facilityItem;
                model.Departmnets = deptItem;
                model.SubDepartmnets = subdeptItem;
                model.Areas = areaItem;




                var hostname = await _repoAsset.IsHostnameAvaiable(model.HostName);

                if (hostname)
                {
                    ModelState.AddModelError("HostName", $"The hostname with name {model.HostName} already exists");
                }

                var ipaddress = await _repoAsset.IsIPAddressAvaiable(model.IPAddress);

                if (ipaddress)
                {
                    ModelState.AddModelError("IPAddress", $"The IP Address with name {model.IPAddress} already exists");
                }


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
                if (model.AreaId == 0)
                {
                    model.AreaId = null;
                }
            

                var asset = _mapper.Map<ITAsset>(model);
                var isSuccess = false;

                if (id == 0)
                {
                    isSuccess = await _repoAsset.Create(asset);
                }
                else
                {
                    isSuccess = await _repoAsset.Update(asset);
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

       

        // GET: ITAssetController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var asset = await _repoAsset.FindById(id);

            if (asset == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoAsset.Delete(asset);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
