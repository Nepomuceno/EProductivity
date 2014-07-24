using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EProductivity.Core.Model.Data;
using EProductivity.Web.Models;

namespace EProductivity.Web.Controllers
{
    [RoutePrefix("responsability")]
    public class ResponsabilitiesController : Controller
    {
        private readonly IModelContext _context;
        private readonly EProductivityUserManager _userManager;

        public ResponsabilitiesController(IModelContext context, EProductivityUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route(""),HttpGet]
        public ActionResult SelectArea()
        {
            return View();
        }

        [Route("{areaId}")]
        public ActionResult Index(int areaId)
        {
            var area = _context.Responsabilities[areaId];
            var areas = _context.Responsabilities.Include(r => r.Area).Where(r => r.AreaId == areaId).Select(r => new ResponsabilityViewModel
            {
                Name = r.Name,
                Id = r.ResponsabilityId,
                Area = r.Area.Name,
                AreaId = r.AreaId
            });
            ViewBag.Area = area;
            return View(areas);
        }

        [Route("new"),HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("new"),HttpPost]
        public ActionResult Create(ResponsabilityViewModel responsability)
        {
            return View();
        }

        [Route("delete"), HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Route("dropdown"),HttpGet,HttpPost]
        public async Task<JsonResult> GetResponsabilityDropDown(int areaId)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _context.Responsabilities.Where(a => a.OrganizationId == currentUser.OrganizationId && a.AreaId == areaId).GroupBy(r => r.Area).Select(a => new Category()
            {
                text = a.Key.Name,
                children = a.Select(r => new Option()
                {
                    id = r.ResponsabilityId.ToString(CultureInfo.InvariantCulture),
                    text = r.Name
                })
            });
            return Json(new DropdownOptions {more = false,results = result}, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
    public class ResponsabilityViewModel
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string Area { get; set; }
        public long AreaId { get; set; }
    }

}