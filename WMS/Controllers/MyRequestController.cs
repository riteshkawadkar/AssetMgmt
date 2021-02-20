using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMS.Contracts;
using WMS.Data;
using WMS.Data.Request;
using WMS.Models;

namespace WMS.Controllers
{
    [Authorize]
    public class MyRequestController : Controller
    {
        private readonly IMyRequestRepository _repoAppRequest;
        private readonly IRequestTypeRepository _repoRequestType;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;

        public MyRequestController(IMyRequestRepository repoAppRequest, IRequestTypeRepository repoRequestType,
            UserManager<Employee> userManager, IMapper mapper)
        {
            _repoAppRequest = repoAppRequest;
            _repoRequestType = repoRequestType;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: AppRequest
        public async Task<ActionResult> Index()
        {
            //var appRequests = await _repoAppRequest.FindAll();
            //get currently signed in user
            var employee = _userManager.GetUserAsync(User).Result;

            var appRequests = await _repoAppRequest.GetAllRequestsByEmployee(employee.Id);

            var appRequstsModel = _mapper.Map<List<MyRequestVM>>(appRequests);
            var model = new RequestDashboardVM
            {
                TotalRequests = appRequstsModel.Count,
                ApprovedRequests = appRequstsModel.Count(q => q.Approved == true),
                PendingRequests = appRequstsModel.Count(q => q.Approved == null),
                RejectedRequests = appRequstsModel.Count(q => q.Approved == false),
                AppRequests = appRequstsModel
            };
            return View(model);
        }

        // GET: AppRequest/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoAppRequest.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }
            return View();
        }

        // GET: AppRequest/Create
        public async Task<ActionResult> Create()
        {
            var requestTypes = await _repoRequestType.FindAll();
            var requestTypeItems = requestTypes.Select(q => new SelectListItem
            {
                Text = q.RequestName,
                Value = q.Id.ToString()
            });

            var model = new CreateAppRequestVM
            {
                RequestTypes = requestTypeItems
            };

            return View(model);
        }

        // POST: AppRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAppRequestVM model)
        {
            try
            {
                var requestTypes = await _repoRequestType.FindAll();
                var requestTypeItems = requestTypes.Select(q => new SelectListItem
                {
                    Text = q.RequestName,
                    Value = q.Id.ToString()
                });

                model.RequestTypes = requestTypeItems;

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View();
                }

                //get currently signed in user
                var employee = _userManager.GetUserAsync(User).Result;

                var appRequestmodel = new MyRequestVM
                {
                    RequestingEmployeeId = employee.Id,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    RequestTypeId = model.RequestTypeId
                };

                var appRequest = _mapper.Map<MyRequest>(appRequestmodel);
                var isSuccess = await _repoAppRequest.Create(appRequest);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View();
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        // GET: AppRequest/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: AppRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppRequest/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

       
    }
}
