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
    public class InstrumentController : Controller
    {
        private readonly ILocationRepository _repoLocation;
        private readonly IFacilityRepository _repoFacility;
        private readonly IDepartmentRepository _repoDepartment;
        private readonly ISubDepartmentRepository _repoSubDepartment;
        private readonly IAreaRepository _repoArea;
        private readonly IRoomRepository _repoRoom;
        private readonly ILineRepository _repoLine;
        private readonly IInstrumentRepository _repoInstrument;
        private readonly IMapper _mapper;

        public InstrumentController(ILocationRepository repoLocation, IFacilityRepository repoFacility, IDepartmentRepository repoDepartment, ISubDepartmentRepository repoSubDepartment,
         IAreaRepository repoArea, IRoomRepository repoRoom, ILineRepository repoLine, IInstrumentRepository repoInstrument, IMapper mapper)
        {
            _repoLocation = repoLocation;
            _repoFacility = repoFacility;
            _repoDepartment = repoDepartment;
            _repoSubDepartment = repoSubDepartment;
            _repoArea = repoArea;
            _repoRoom = repoRoom;
            _repoLine = repoLine;
            _repoInstrument = repoInstrument;
            _mapper = mapper;
        }

        // GET: InstrumentController
        public async Task<ActionResult> Index()
        {
            var instrumentList = await _repoInstrument.FindAll();
            var model = _mapper.Map<IEnumerable<Instrument>, IEnumerable<InstrumentVM>>(instrumentList);
            return View(model);
        }

        // GET: InstrumentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoInstrument.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var instrument = await _repoInstrument.FindById(id);

            var location = await _repoLocation.FindById(instrument.LocationId);
            var locationModel = _mapper.Map<LocationVM>(location);

            var facility = await _repoFacility.FindById((int)instrument.FacilityId);
            var facilityModel = _mapper.Map<FacilityVM>(facility);

            var department = await _repoDepartment.FindById((int)instrument.DepartmentId);
            var deptModel = _mapper.Map<DepartmentVM>(department);

            var subdepartment = await _repoDepartment.FindById((int)instrument.SubDepartmentId);
            var subdeptModel = _mapper.Map<SubDepartmentVM>(subdepartment);

            var darea = await _repoArea.FindById((int)instrument.AreaId);
            var areaModel = _mapper.Map<AreaVM>(darea);

            var room = await _repoRoom.FindById((int)instrument.RoomId);
            var roomModel = _mapper.Map<RoomVM>(room);

            var line = await _repoRoom.FindById((int)instrument.LineId);
            var lineModel = _mapper.Map<LineVM>(line);

            var model = _mapper.Map<InstrumentVM>(line);
            model.Location = locationModel;
            model.Facility = facilityModel;
            model.Department = deptModel;
            model.SubDepartment = subdeptModel;
            model.Area = areaModel;
            model.Room = roomModel;
            model.Line = lineModel;


            return View(model);
        }

        // GET: InstrumentController/Create
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


            var lineList = await _repoLine.FindAll(); //get list of companies
                                                // a way to covert companyList Company to SelectListItem
            var lineItem = lineList.Select(q => new SelectListItem
            {
                Text = q.LineName,
                Value = q.Id.ToString()
            });

            var model = new InstrumentVM
            {
                Locations = locationItem,
                Facilities = facilityItem,
                Departmnets = deptItem,
                SubDepartmnets = subdeptItem,
                Areas = areaItem,
                Rooms = roomItem,
                Lines = lineItem
            };

            //CREATE
            if (id == 0)
            {
                return View(model);
            }
            //EDIT
            else
            {
                var isExists = await _repoInstrument.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var equip = await _repoInstrument.FindById(id);

                model.Id = equip.Id;
                model.InstrumentName = equip.InstrumentName;
                model.LocationId = equip.LocationId;

                if (model.FacilityId == 0)
                    model.FacilityId = null;
                else
                    model.FacilityId = equip.FacilityId;

                if (model.DepartmentId == 0)
                    model.DepartmentId = null;
                else
                    model.DepartmentId = equip.DepartmentId;

                if (model.SubDepartmentId == 0)
                    model.SubDepartmentId = null;
                else
                    model.SubDepartmentId = equip.SubDepartmentId;

                if (model.AreaId == 0)
                    model.AreaId = null;
                else
                    model.AreaId = equip.AreaId;

                if (model.RoomId == 0)
                    model.RoomId = null;
                else
                    model.RoomId = equip.RoomId;

                if (model.LineId == 0)
                    model.LineId = null;
                else
                    model.LineId = equip.LineId;

                return View(model);
            }
        }

        // POST: EquipmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(int id, EquipmentVM model)
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


                var lineList = await _repoLine.FindAll(); //get list of companies
                                                    // a way to covert companyList Company to SelectListItem
                var lineItem = lineList.Select(q => new SelectListItem
                {
                    Text = q.LineName,
                    Value = q.Id.ToString()
                });

                model.Locations = locationItemList;
                model.Facilities = facilityItem;
                model.Departmnets = deptItem;
                model.SubDepartmnets = subdeptItem;
                model.Areas = areaItem;
                model.Rooms = roomItem;
                model.Lines = lineItem;

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
                if (model.RoomId == 0)
                {
                    model.RoomId = null;
                }
                if (model.LineId == 0)
                {
                    model.LineId = null;
                }

                var equip = _mapper.Map<Instrument>(model);
                var isSuccess = false;

                if (id == 0)
                {
                    isSuccess = await _repoInstrument.Create(equip);
                }
                else
                {
                    isSuccess = await _repoInstrument.Update(equip);
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



        // GET: EquipmentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var equip = await _repoInstrument.FindById(id);

            if (equip == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoInstrument.Delete(equip);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
