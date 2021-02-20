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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WMS.Data.Request;
using System.Linq.Dynamic;

namespace WMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RequestTypeController : Controller
    {
        private readonly IRequestTypeRepository _repoRequestType;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RequestTypeController(IRequestTypeRepository repoRequestType, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _repoRequestType = repoRequestType;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // GET: Company/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            
            var requestTypeList = await _repoRequestType.FindAll();

            string[] ApproversList = new string[requestTypeList.Count()];
            int i = 0;
            var RName = "RName";

            foreach (var rType in requestTypeList)
            {
                ApproversList[i] = requestTypeList.Where(x => x.RequestName == rType.RequestName).Select(x => new { x.Approvers }).FirstOrDefault().Approvers;
                RName = rType.RequestName;
                break;
            }


            var roles = await _roleManager.Roles.ToListAsync();
            SelectList RolesList = new SelectList(roles);
            ViewBag.RolesList = RolesList;
            ViewBag.RName = RName;

            var model = new RequestTypeEditVM
            {
                RoleNameList = roles,
                RoleApprovers = ApproversList
            };

            

            return View(model);
        }

        // GET: RequestTypeController/Create
        public async Task<ActionResult> Index()
        {
            var requestTypeList = await _repoRequestType.FindAll();
            var model = _mapper.Map<IEnumerable<RequestType>, IEnumerable<RequestTypeVM>>(requestTypeList);

            string[] ApproversList = new string[requestTypeList.Count()];
            int i = 0;

            foreach (var requestType in requestTypeList)
            {
                ApproversList[i] = requestTypeList.Where(x => x.RequestName == requestType.RequestName).Select(x => new { x.Approvers }).FirstOrDefault().Approvers;
                i++;
            }

            //var approvers = requestTypeList.Where(x => x.RequestName == "Password Change").Select(x => new { x.Approvers }).ToString(); ;

            var roles = await _roleManager.Roles.ToListAsync();
            SelectList RolesList = new SelectList(roles);
            ViewBag.RolesList = RolesList;

            var modelRequestType = new RequestTypeVM()
            {
                RequestNameList = requestTypeList,
                RoleNameList = roles,
                RoleApprovers = ApproversList
            };

            return View(modelRequestType);
        }

        

        // GET: RequestTypeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var et = await _repoRequestType.FindById(id);

            if (et == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoRequestType.Delete(et);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }



        // POST: RequestTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RequestTypeVM model, int id = 0)
        {
            try
            {

                var requestType = _mapper.Map<RequestType>(model);
                var isSuccess = false;

                //Index
                if (id == 0)
                {
                    isSuccess = await _repoRequestType.Create(requestType);

                    if (!isSuccess)
                    {
                        ModelState.AddModelError("", "Something Went Wrong...");
                        return View(model);
                    }
                }
                //Apply Changes
                else
                {
                    isSuccess = await _repoRequestType.Create(requestType);

                    if (!isSuccess)
                    {
                        ModelState.AddModelError("", "Something Went Wrong...");
                        return View(model);
                    }
                }



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


       
    }
}
