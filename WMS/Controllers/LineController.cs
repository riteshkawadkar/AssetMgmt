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
    public class LineController : Controller
    {
        private readonly ILocationRepository _repoLocation;
        private readonly IFacilityRepository _repoFacility;
        private readonly IDepartmentRepository _repoDepartment;
        private readonly ISubDepartmentRepository _repoSubDepartment;
        private readonly IAreaRepository _repoArea;
        private readonly IRoomRepository _repoRoom;
        private readonly ILineRepository _repoLine;
        private readonly IMapper _mapper;

        public LineController(ILocationRepository repoLocation, IFacilityRepository repoFacility, IDepartmentRepository repoDepartment, ISubDepartmentRepository repoSubDepartment,
         IAreaRepository repoArea, IRoomRepository repoRoom, ILineRepository repoLine, IMapper mapper)
        {
            _repoLocation = repoLocation;
            _repoFacility = repoFacility;
            _repoDepartment = repoDepartment;
            _repoSubDepartment = repoSubDepartment;
            _repoArea = repoArea;
            _repoRoom = repoRoom;
            _repoLine = repoLine;
            _mapper = mapper;
        }

        // GET: LineController
        public async Task<ActionResult> Index()
        {
            var lineList = await _repoLine.FindAll();
            var model = _mapper.Map<IEnumerable<Line>, IEnumerable<LineVM>>(lineList);
            return View(model);
        }

        // GET: LineController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoLine.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var line = await _repoLine.FindById(id);

            var location = await _repoLocation.FindById(line.LocationId);
            var locationModel = _mapper.Map<LocationVM>(location);

            var facility = await _repoFacility.FindById((int)line.FacilityId);
            var facilityModel = _mapper.Map<FacilityVM>(facility);

            var department = await _repoDepartment.FindById((int)line.DepartmentId);
            var deptModel = _mapper.Map<DepartmentVM>(department);

            var subdepartment = await _repoDepartment.FindById((int)line.SubDepartmentId);
            var subdeptModel = _mapper.Map<SubDepartmentVM>(subdepartment);

            var darea = await _repoArea.FindById((int)line.AreaId);
            var areaModel = _mapper.Map<AreaVM>(darea);

            var room = await _repoRoom.FindById((int)line.RoomId);
            var roomModel = _mapper.Map<RoomVM>(room);

            var model = _mapper.Map<LineVM>(line);
            model.Location = locationModel;
            model.Facility = facilityModel;
            model.Department = deptModel;
            model.SubDepartment = subdeptModel;
            model.Area = areaModel;
            model.Room = roomModel;
            

            return View(model);
        }

        // GET: LineController/Create
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

            var roomList = await _repoRoom.FindAll(); //get list of companies
                                                // a way to covert companyList Company to SelectListItem
            var roomItem = roomList.Select(q => new SelectListItem
            {
                Text = q.RoomName,
                Value = q.Id.ToString()
            });

            var model = new LineVM
            {
                Locations = locationItem,
                Facilities = facilityItem,
                Departmnets = deptItem,
                SubDepartmnets = subdeptItem,
                Areas = areaItem,
                Rooms = roomItem
            };

            //CREATE
            if (id == 0)
            {
                return View(model);
            }
            //EDIT
            else
            {
                var isExists = await _repoLine.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var room = await _repoLine.FindById(id);

                model.Id = room.Id;
                model.LineName = room.LineName;
                model.LocationId = room.LocationId;
                model.RoomId = room.RoomId;

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

        // POST: LineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(int id, LineVM model)
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


                var roomlist = await _repoRoom.FindAll(); //get list of companies
                                                    // a way to covert companyList Company to SelectListItem
                var roomItem = roomlist.Select(q => new SelectListItem
                {
                    Text = q.RoomName,
                    Value = q.Id.ToString()
                });

                model.Locations = locationItemList;
                model.Facilities = facilityItem;
                model.Departmnets = deptItem;
                model.SubDepartmnets = subdeptItem;
                model.Areas = areaItem;
                model.Rooms = roomItem;

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
                

                var line = _mapper.Map<Line>(model);
                var isSuccess = false;

                if (id == 0)
                {
                    isSuccess = await _repoLine.Create(line);
                }
                else
                {
                    isSuccess = await _repoLine.Update(line);
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

        

        // GET: LineController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var line = await _repoLine.FindById(id);

            if (line == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoLine.Delete(line);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("api/lfacility/{LocationId}")]
        public async Task<IEnumerable<Facility>> FacilityAsync(int LocationId)
        {
            return await _repoFacility.GetFacilitiesByLocation(LocationId);
        }

        [HttpGet("api/ldepartment/{FacilityId}")]
        public async Task<IEnumerable<Department>> DepartmentAsync(int FacilityId)
        {
            return await _repoDepartment.GetDepartmentsByFacility(FacilityId);
        }

        [HttpGet("api/lsubdepartment/{DepartmentId}")]
        public async Task<IEnumerable<SubDepartment>> SubDepartmentAsync(int DepartmentId)
        {
            return await _repoSubDepartment.GetSubDepartmentsByDept(DepartmentId);
        }

        [HttpGet("api/lArea/{DepartmentId}")]
        public async Task<IEnumerable<DArea>> AreaAsync(int DepartmentId)
        {
            return await _repoArea.GetAreasByDepartment(DepartmentId);
        }


        [HttpGet("api/lRoom/{AreaId}")]
        public async Task<IEnumerable<Room>> RoomAsync(int AreaId)
        {
            return await _repoRoom.GetRoomsByArea(AreaId);
        }

    }
}
