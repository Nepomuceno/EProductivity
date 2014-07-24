using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EProductivity.Core.Model;
using EProductivity.Core.Model.Data;

namespace EProductivity.Web.Controllers
{
    [RoutePrefix("worksample")]
    public class WorkSamplesController : AsyncController
    {
        private readonly IModelContext _context;

        public WorkSamplesController(IModelContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [Route("opened")]
        public ActionResult OpenList()
        {
            var samples = new List<WorkSampleViewModel>
            {
                new WorkSampleViewModel()
                {
                    Id = 1,
                    Name = "Amostra 1",
                    Area = "Administrativo",
                    CompletedTours = 10,
                    StartDate = DateTime.Now.AddDays(-5),
                    TotalWorkers = 10
                },
                new WorkSampleViewModel()
                {
                    Id = 2,
                    Name = "Amostra 2",
                    Area = "Administrativo",
                    CompletedTours = 5,
                    StartDate = DateTime.Now.AddDays(-3),
                    TotalWorkers = 8
                },
                new WorkSampleViewModel()
                {
                    Id = 3,
                    Name = "Amostra 3",
                    Area = "Produção",
                    CompletedTours = 6,
                    StartDate = DateTime.Now.AddDays(-2),
                    TotalWorkers = 21
                }
            };
            return PartialView(samples);
        }
        [Route("details/{workSampleId}")]
        public ActionResult Details(long workSampleId)
        {
            var workSample = _context.WorkSamples[workSampleId];
            var detail = new WorkSampleViewModel(workSample);
            return View(detail);
        }
    }

    public class WorkSampleViewModel
    {
        public WorkSampleViewModel()
        {
            
        }

        public WorkSampleViewModel(WorkSample workSample)
        {
            
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int CompletedTours { get; set; }
        public int TotalWorkers { get; set; }
        public string Area { get; set; }
        public IEnumerable<TourViewModel> Tours { get; set; }

    }

    public class TourViewModel
    {
        public long Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalObservations { get; set; }
        public IEnumerable<ObservationViewModel> Observations { get; set; }

    }

    public class ObservationViewModel
    {
    }
}