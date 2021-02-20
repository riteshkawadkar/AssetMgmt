using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMS.Data;

namespace WMS.Controllers
{
    public class RequestFlowController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RequestFlowController(IMapper mapper, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _db = db;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        // GET: RequestFlowController
        public ActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // GET: RequestFlowController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RequestFlowController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestFlowController/Create
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

        // GET: RequestFlowController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RequestFlowController/Edit/5
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

        // GET: RequestFlowController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RequestFlowController/Delete/5
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
