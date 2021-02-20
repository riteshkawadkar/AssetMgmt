using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMS.Contracts.Workflow;
using WMS.Data.Workflow;
using WMS.Models.Worflow;

namespace WMS.Controllers.Workflow
{
    public class RequestWorkflowController : Controller
    {
        private readonly IRequestWorfklowRepository _repoRequestWorkflow;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RequestWorkflowController(IRequestWorfklowRepository repoRequestWorkflow, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _repoRequestWorkflow = repoRequestWorkflow;
            _roleManager = roleManager;
            _mapper = mapper;
        }


        // GET: RequestWorkflow
        public async Task<ActionResult> Index()
        {
            var reqWorkflowList = await _repoRequestWorkflow.FindAll();
            var model = _mapper.Map<IEnumerable<RequestWorkflow>, IEnumerable<RequestWorkflowVM>>(reqWorkflowList);
            return View(model);
        }

        // GET: RequestWorkflow/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RequestWorkflow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestWorkflow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: RequestWorkflow/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RequestWorkflow/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: RequestWorkflow/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RequestWorkflow/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
