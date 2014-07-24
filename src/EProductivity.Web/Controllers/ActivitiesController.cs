using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EProductivity.Core.Model.Data;
using EProductivity.Web.Models;

namespace EProductivity.Web.Controllers
{
    [RoutePrefix("activity")]
    public class ActivitiesController : AsyncController
    {
        private readonly IModelContext _context;
        private readonly EProductivityUserManager _userManager;

        public ActivitiesController(IModelContext context, EProductivityUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("")]
        public ActionResult Index()
        {
            var activities = _context.Activities.Select(a => new ActivityViewModel
            {
                Name = a.Name,
                Id = a.ActivityId
            });
            return View(activities);
        }

        [Route("new"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("new"), HttpPost]
        public ActionResult Create(ActivityViewModel activity)
        {
            return View();
        }

        [Route("delete"), HttpGet]
        public async Task<ActionResult> Delete(long id)
        {
            var activity = _context.Activities[id];
            _context.Activities.Remove(activity);
            await _context.SaveAsync();
            return RedirectToAction("Index");
        }

        [Route("dropdown"), HttpGet]
        public async Task<ActionResult> GetResponsabilityDropDown()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _context.Activities.Where(a => a.OrganizationId == currentUser.OrganizationId).Select(a => new Option()
            {
                id = a.ActivityId.ToString(CultureInfo.InvariantCulture),
                text = a.Name
            });
            return Json(new DropdownOptions {
                                                more = false,
                                                results = new List<Category>{new Category()
                                                                        {
                                                                            text = "Atividades",
                                                                            children = result
                                                                        }
                                            }
            }, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
    public class ActivityViewModel
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public bool BaseActivity { get; set; }
    }

}