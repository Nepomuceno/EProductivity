using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        public ActionResult Create(AreaViewModel area)
        {
            return View();
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
    }


}