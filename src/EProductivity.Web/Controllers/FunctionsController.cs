using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EProductivity.Core.Model;
using EProductivity.Core.Model.Data;
using EProductivity.Web.Models;

namespace EProductivity.Web.Controllers
{
    [RoutePrefix("function")]
    public class FunctionsController : AsyncController
    {
        private readonly IModelContext _context;
        private readonly EProductivityUserManager _userManager;

        public FunctionsController(IModelContext context, EProductivityUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("")]
        public ActionResult Index()
        {
            var areas = _context.Functions.Include(r => r.Area).Select(r => new FunctionViewModel
            {
                Name = r.Name,
                Id = r.FunctionId,
                Area = r.Area.Name,
                AreaId = r.AreaId
            });
            return View(areas);
        }

        [Route("new"),HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("new"),HttpPost]
        public async Task<ActionResult> Create(FunctionViewModel function)
        {
            _context.Functions.Add(new Function()
            {
                AreaId = function.AreaId,
                Name = function.Name,
                OrganizationId = (await _userManager.FindByNameAsync(User.Identity.Name)).OrganizationId
            });
            await _context.SaveAsync();
            return RedirectToAction("Index");
        }

        [Route("delete"), HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Route("dropdown"),HttpGet,HttpPost]
        public async Task<JsonResult> GetFunctionsDropDown(string q)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _context.Functions.Where(a => a.OrganizationId == currentUser.OrganizationId && 
                (q == "" || a.Name.Contains(q))).GroupBy(r => r.Area).Select(a => new Category()
            {
                text = a.Key.Name,
                children = a.Select(r => new Option()
                {
                    id = r.FunctionId.ToString(CultureInfo.InvariantCulture),
                    text = r.Name
                })
            });
            return Json(new DropdownOptions {more = false,results = result}, "application/json", JsonRequestBehavior.AllowGet);
        }
    }

}