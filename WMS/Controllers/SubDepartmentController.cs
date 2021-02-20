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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;

namespace WMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubDepartmentController : Controller
    {
        private readonly ISubDepartmentRepository _repoSubDepartment;
        private readonly IDepartmentRepository _repoDepartment;
        private readonly IMapper _mapper;

        public SubDepartmentController(IDepartmentRepository repoDepartment,
            ISubDepartmentRepository repoSubDepartment,
            IMapper mapper)
        {
            _repoDepartment = repoDepartment;
            _repoSubDepartment = repoSubDepartment;
            _mapper = mapper;
        }
        // GET: SubDepartment
        public async Task<ActionResult> Index()
        {

            var subDeptLocationList = await _repoSubDepartment.FindAll();
            var model = _mapper.Map<IEnumerable<SubDepartment>, IEnumerable<SubDepartmentVM>>(subDeptLocationList);
            return View(model);

        }

        // GET: SubDepartment/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoSubDepartment.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }


            var sdept = _repoSubDepartment.FindById(id);
            var model = _mapper.Map<SubDepartmentVM>(sdept);

            return View(model);
        }

        
        

        // GET: SubDepartment/Create
        public async Task<ActionResult> AddEdit(int id = 0)
        {
       
            //fetch Department id
            var deptList = await _repoDepartment.FindAll(); 
            var depListItem = deptList.Select(q => new SelectListItem
            {
                Text = q.DepartmentName,
                Value = q.Id.ToString()
            });
            

            var model = new SubDepartmentVM
            {
                Departments = depListItem
            };

            //CREATE
            if (id == 0)
            {
                return View(model);
            }
            //EDIT
            else
            {
                var isExists = await _repoSubDepartment.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var subDept = await _repoSubDepartment.FindById(id);

                model.Id = subDept.Id;
                model.SubDepartmentName = subDept.SubDepartmentName;

                model.DepartmentId = subDept.DepartmentId;


                return View(model);
            }
        }

        // POST: SubDepartment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(int id, SubDepartmentVM model)
        {
            try
            {
                // TODO: Add insert logic here
           
                var depList = await _repoDepartment.FindAll(); 
                var depsItem = depList.Select(q => new SelectListItem
                {
                    Text = q.DepartmentName,
                    Value = q.Id.ToString()
                });

                model.Departments = depsItem;


                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var subdepts = _mapper.Map<SubDepartment>(model);

                var isSuccess = false;

                if (id == 0)
                {
                    isSuccess = await _repoSubDepartment.Create(subdepts);
                }
                else
                {
                    isSuccess = await _repoSubDepartment.Update(subdepts);
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

     
        // GET: SubDepartment/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var sdept = await _repoSubDepartment.FindById(id);

            if (sdept == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoSubDepartment.Delete(sdept);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("api/department/{LocationId}")]
        [HttpGet]
        public async Task<IEnumerable<Department>> DepartmentByLocation(int LocationId)
        {

            return await _repoDepartment.GetDepartmentsByLocation(LocationId);

        }

        [Route("api/department/{FacilityId:int}")]
        [HttpGet]
        public async Task<IEnumerable<Department>> DepartmentByFacility(int FacilityId)
        {

            return await _repoDepartment.GetDepartmentsByFacility(FacilityId);

        }


    }
}