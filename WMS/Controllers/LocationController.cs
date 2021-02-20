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
    public class LocationController : Controller
    {
        private readonly ICompanyRepository _repoCompany;
        private readonly ILocationRepository _repoLocation;
        private readonly IMapper _mapper;

        public LocationController(ICompanyRepository repoCompany, ILocationRepository repoLocation, IMapper mapper)
        {
            _repoCompany = repoCompany;
            _repoLocation = repoLocation;
            _mapper = mapper;
        }

        // GET: Location
        public async Task<ActionResult> Index()
        {

            var locationList = await _repoLocation.FindAll(); //get list of locations
            var model = _mapper.Map<IEnumerable<Location>, IEnumerable<LocationVM>>(locationList);
            return View(model);



        }

        // GET: Location/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repoLocation.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }

            var location = await _repoLocation.FindById(id);

            var company = _repoCompany.FindById(location.CompanyID);
            var companyModel = _mapper.Map<CompanyVM>(company);

            var model = _mapper.Map<LocationVM>(location);
            model.Company = companyModel;


            return View(model);
        }

        // GET: Location/Create
        public async Task<ActionResult> Create()
        {
            //if we don't use SelectListItem and just use the Class type like Company
            //the we can use below code
            /*
            var model = new LocationVM
            {
                Companies = _repoCompany.FindAll()
            };

            @Html.DropDownListFor(q => q.CompanyID, new SelectList(Model.Companies, "Id", "CompanyName"), "Select Company", new { @class = "form-control" })
            */


            var companyList = await _repoCompany.FindAll(); //get list of companies
            // a way to covert companyList Company to SelectListItem
            var companiesItem = companyList.Select(q => new SelectListItem
            {
                Text = q.CompanyName,
                Value = q.Id.ToString()
            });

            var model = new LocationVM
            {
                Companies = companiesItem
            };
            return View(model);

        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LocationVM model)
        {
            try
            {
                var companyList = await _repoCompany.FindAll(); //get list of companies
                var companiesItem = companyList.Select(q => new SelectListItem
                {
                    Text = q.CompanyName,
                    Value = q.Id.ToString()
                });

                model.Companies = companiesItem;

                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    //returning view with same data
                    return View(model);
                }
                //map LocationVM model to Location Data Model
                var locations = _mapper.Map<Location>(model);

                //handle DateCreated as it is not avaialble in form
                //locations.DateCreated = DateTime.Now;



                var isSuccess = await _repoLocation.Create(locations);

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
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: Location/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var isExists = await _repoLocation.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }
            var location = await _repoLocation.FindById(id);
            var companyList = await _repoCompany.FindAll(); //get list of companies
            // a way to covert companyList Company to SelectListItem
            var companiesItem = companyList.Select(q => new SelectListItem
            {
                Text = q.CompanyName,
                Value = q.Id.ToString()
            });

            

            LocationVM model = new LocationVM();
            model.Id = location.Id;
            model.LocationName = location.LocationName;
            model.Companies = companiesItem;
            model.CompanyID = location.CompanyID;
            model.AddressLine1 = location.AddressLine1;
            model.AddressLine2 = location.AddressLine2;
            model.AddressLine3 = location.AddressLine3;
            model.AddressLine4 = location.AddressLine4;
            model.AddressLine5 = location.AddressLine5;
            model.Country = location.Country;
            model.State = location.State;
            model.City = location.City;
            model.PinCode = location.PinCode;
            //model.DateCreated = location.DateCreated;
            //model.DateModified = DateTime.Now;
     
            return View(model);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LocationVM model)
        {
            try
            {
                var companyList = await _repoCompany.FindAll(); //get list of companies
                var companiesItem = companyList.Select(q => new SelectListItem
                {
                    Text = q.CompanyName,
                    Value = q.Id.ToString()
                });

                model.Companies = companiesItem;

                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {
                    //returning view with same data
                    return View(model);
                }

                //map ComanyVM model to Company Data Model
                var location = _mapper.Map<Location>(model);

                //handle DateCreated as it is not avaialble in form
                //location.DateModified = DateTime.Now;

                var isSuccess = await _repoLocation.Update(location);

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

        // GET: Location/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var location = await _repoLocation.FindById(id);

            if (location == null)
            {
                return NotFound();
            }
            var isSuccess = await _repoLocation.Delete(location);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        
    }
}