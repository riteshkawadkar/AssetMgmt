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
    public class RoomController : Controller
    {
        private readonly ILocationRepository _repoLocation;
        private readonly IFacilityRepository _repoFacility;
        private readonly IDepartmentRepository _repoDepartment;
        private readonly ISubDepartmentRepository _repoSubDepartment;
        private readonly IAreaRepository _repoArea;
        private readonly IRoomRepository _repoRoom;
        private readonly IMapper _mapper;

        public RoomController(ILocationRepository repoLocation, IFacilityRepository repoFacility, IDepartmentRepository repoDepartment, ISubDepartmentRepository repoSubDepartment,
            IAreaRepository repoArea, IRoomRepository repoRoom, IMapper mapper)
        {
            _repoLocation = repoLocation;
            _repoFacility = repoFacility;
            _repoDepartment = repoDepartment;
            _repoSubDepartment = repoSubDepartment;
            _repoArea = repoArea;
            _repoRoom = repoRoom;
            _mapper = mapper;
        }

        // GET: RoomController
        public async Task<ActionResult> Index()
        {
            var roomList = await _repoRoom.FindAll();
            var model = _mapper.Map<IEnumerable<Room>, IEnumerable<RoomVM>>(roomList);
            return View(model);
        }

        // GET: RoomController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoRoom.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var room = await _repoRoom.FindById(id);

            var location = _repoLocation.FindById(room.LocationId);
            var locationModel = _mapper.Map<LocationVM>(location);

            var facility = _repoFacility.FindById((int)room.FacilityId);
            var facilityModel = _mapper.Map<FacilityVM>(facility);

            var department = _repoDepartment.FindById((int)room.DepartmentId);
            var deptModel = _mapper.Map<DepartmentVM>(department);

            var subdepartment = _repoDepartment.FindById((int)room.SubDepartmentId);
            var subdeptModel = _mapper.Map<SubDepartmentVM>(subdepartment);

            var darea = _repoArea.FindById((int)room.AreaId);
            var areaModel = _mapper.Map<AreaVM>(darea);

            var model = _mapper.Map<RoomVM>(room);
            model.Location = locationModel;
            model.Facility = facilityModel;
            model.Department = deptModel;
            model.SubDepartment = subdeptModel;
            model.DArea = areaModel;
            

            return View(model);
        }

        // GET: RoomController/Create
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

            var model = new RoomVM
            {
                Locations = locationItem,
                Facilities = facilityItem,
                Departmnets = deptItem,
                SubDepartmnets = subdeptItem,
                Areas = areaItem
            };

            //CREATE
            if (id == 0)
            {
                return View(model);
            }
            //EDIT
            else
            {
                var isExists = await _repoRoom.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var room = await _repoRoom.FindById(id);

                model.Id = room.Id;
                model.RoomName = room.RoomName;
                model.LocationId = room.LocationId;

                if (model.FacilityId == 0)
                    model.FacilityId = null;
                else
                    model.FacilityId = room.FacilityId;

                if (model.DepartmentId == 0)
                    model.DepartmentId = null;
                else
                    model.DepartmentId = room.DepartmentId;

                if (model.SubDepartmentId == 0)
                    model.SubDepartmentId = null;
                else
                    model.SubDepartmentId = room.SubDepartmentId;

                if (model.AreaId == 0)
                    model.AreaId = null;
                else
                    model.AreaId = room.AreaId;

                return View(model);
            }
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(int id, RoomVM model)
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

                var room = _mapper.Map<Room>(model);
                var isSuccess = false;

                if (id == 0)
                {
                    isSuccess = await _repoRoom.Create(room);
                }
                else
                {
                    isSuccess = await _repoRoom.Update(room);
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

       

        // GET: RoomController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var room = await _repoRoom.FindById(id);

            if (room == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoRoom.Delete(room);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("api/rfacility/{LocationId}")]
        public async Task<IEnumerable<Facility>> Facility(int LocationId)
        {
            return await _repoFacility.GetFacilitiesByLocation(LocationId);
        }

        [HttpGet("api/rdepartment/{FacilityId}")]
        public async Task<IEnumerable<Department>> Department(int FacilityId)
        {
            return await _repoDepartment.GetDepartmentsByFacility(FacilityId);
        }

        [HttpGet("api/rsubdepartment/{DepartmentId}")]
        public async Task<IEnumerable<SubDepartment>> SubDepartment(int DepartmentId)
        {
            return await _repoSubDepartment.GetSubDepartmentsByDept(DepartmentId);
        }

        [HttpGet("api/rArea/{DepartmentId}")]
        public async Task<IEnumerable<DArea>> AreaAsync(int DepartmentId)
        {
            return await _repoArea.GetAreasByDepartment(DepartmentId);
        }

    }
}
