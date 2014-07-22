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

        [Route("")]
        public ActionResult Index()
        {
            var areas = _context.Areas.Select(a => new ResponsabilityViewModel
            {
                Name = a.Name,
                Id = a.AreaId
            });
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
        public async Task<JsonResult> GetResponsabilityDropDown()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _context.Responsabilities.Where(a => a.OrganizationId == currentUser.OrganizationId).GroupBy(r => r.Area).Select(a => new Category()
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
        public int Id { get; set; }
    }

}