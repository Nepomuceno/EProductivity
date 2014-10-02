using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EProductivity.Core.Model;
using EProductivity.Core.Model.Data;
using EProductivity.Web.Models;
using Microsoft.Ajax.Utilities;

namespace EProductivity.Web.Controllers
{
    [RoutePrefix("workers")]
    public class WorkersController : AsyncController
    {
        private readonly IModelContext _context;
        private readonly EProductivityUserManager _userManager;

        public WorkersController(IModelContext context, EProductivityUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("")]
        public ActionResult Index()
        {
            var workers = _context.Workers.Select(w => new WorkerViewModel()
            {
                Name = w.Name,
                Id = w.WorkerId,
                Area = w.Function.Area.Name,
                Function = w.Function.Name
            });
            return View(workers);
        }

        [Route("new"),HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("new"),HttpPost]
        public async Task<ActionResult> Create(WorkerViewModel worker)
        {
            _context.Workers.Add(new Worker()
            {
                Name = worker.Name,
                FunctionId = worker.FucntionId,
                OrganizationId = (await _userManager.FindByNameAsync(User.Identity.Name)).OrganizationId
            });
            await _context.SaveAsync();
            return View();
        }

        [Route("delete"), HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Route("dropdown"),HttpGet,HttpPost]
        public async Task<JsonResult> GetWorkersDropDown()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _context.Workers.Where(w => w.OrganizationId == currentUser.OrganizationId).Include(w => w.Function.Area)
                .GroupBy(w => w.Function.Area).Select(g => new Category
                {
                    text = g.Key.Name,
                    children = g.Select(w => new Option
                    {
                        id = w.WorkerId.ToString(CultureInfo.InvariantCulture),
                        text = w.Name
                    })
                });
            return Json(new DropdownOptions {more = false,results = result}, "application/json", JsonRequestBehavior.AllowGet);
        }



    }
}