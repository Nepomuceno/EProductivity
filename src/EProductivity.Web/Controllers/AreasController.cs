using System.Collections.Generic;
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
    [RoutePrefix("area")]
    public class AreasController : Controller
    {
        private readonly IModelContext _context;
        private readonly EProductivityUserManager _userManager;

        public AreasController(IModelContext context, EProductivityUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("")]
        public ActionResult Index()
        {
            var areas = _context.Areas.Select(a => new AreaViewModel
            {
                Name = a.Name,
                Id = a.AreaId,
                TotalResponsabilities = a.Responsabilities.Count
            });
            return View(areas);
        }

        [Route("new"),HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("new"),HttpPost]
        public async Task<ActionResult> Create(AreaViewModel area)
        {
            _context.Areas.Add(new Area()
            {
                Name = area.Name,
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
        public async Task<JsonResult> GetAreasDropDown()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _context.Areas.Where(a => a.OrganizationId == currentUser.OrganizationId).Select(a => new Option(){id = a.AreaId.ToString(CultureInfo.InvariantCulture), text = a.Name});
            return Json(new DropdownOptions {more = false,results = new List<Category>{new Category {text = "Areas", children = result}}}, "application/json", JsonRequestBehavior.AllowGet);
        }
    }

    public class AreaViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int TotalResponsabilities { get; set; }
        public IEnumerable<ResponsabilityViewModel> Responsabilities { get; set; }
    }


}