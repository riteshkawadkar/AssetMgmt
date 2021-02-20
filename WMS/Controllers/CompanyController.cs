using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.Contracts;
using WMS.Data;
using WMS.Models;

namespace WMS.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _repo;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: Company
        public async Task<ActionResult> Index()
        {
            var companyList = await _repo.FindAll();
            var model = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyVM>>(companyList);
            return View(model);
        }

        // GET: Company/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repo.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var company = await _repo.FindById(id);
            var model = _mapper.Map<CompanyVM>(company);
            return View(model);
        }

        // GET: Company/Create
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Create()
        {
            //check of Company exists
            var companies = await _repo.FindAll();
            if (companies.ToList().Count > 0)
            {
                ViewBag.Message = "You are entitled to only create 1 Company";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Create(CompanyVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    //returning view with same data
                    return View(model);
                }

                //map CompanyCreateVM model to Company Data Model
                var companies = _mapper.Map<Company>(model);

                //handle DateCreated as it is not avaialble in form
                //companies.DateCreated = DateTime.Now;

                var isSuccess = await _repo.Create(companies);

                if (!isSuccess)
                {
                    //Send Error
                    ModelState.AddModelError("", "Something Went Wrong...");
                    //returning view with same data
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //Send Error
                ModelState.AddModelError("", "Something Went Wrong...");
                //returning view with same data
                return View(model);
            }
        }

        // GET: Company/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Edit(int id)
        {
            var isExists = await _repo.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var company = _repo.FindById(id);
            var model = _mapper.Map<CompanyVM>(company);

            return View(model);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Edit(int id, CompanyVM model)
        {
            try
            {
                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {
                    //returning view with same data
                    return View(model);
                }

                //map ComanyVM model to Company Data Model
                var company = _mapper.Map<Company>(model);

                //handle DateCreated as it is not avaialble in form
                //company.DateModified = DateTime.Now;

                var isSuccess = await _repo.Update(company);

                if (!isSuccess)
                {
                    //Send Error
                    ModelState.AddModelError("", "Something Went Wrong...");
                    //returning view with same data
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //Send Error
                ModelState.AddModelError("", "Something Went Wrong...");
                //returning view with same data
                return View(model);
            }
        }

        // GET: Company/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Delete(int id)
        {
            var company = await _repo.FindById(id);

            if (company == null)
            {
                return NotFound();
            }
            var isSuccess = await _repo.Delete(company);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        
    }
}