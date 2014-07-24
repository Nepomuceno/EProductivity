using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using EProductivity.Core.Model.Data;
using Point = DotNet.Highcharts.Options.Point;

namespace EProductivity.Web.Controllers
{
    [RoutePrefix("charts")]
    [Authorize]
    public class ChartController : Controller
    {
        private readonly IModelContext _context;

        public ChartController(IModelContext context)
        {
            _context = context;
        }

        [Route("procutividadeGeral")]
        public ActionResult ProdutividadeGeral()
        {
            var points = _context.Observations
                .Select(
                    o => o.Activity.ActivityResponsabilities
                        .FirstOrDefault(ar => ar.ResponsabilityId == o.ResponsabilityId))
                .GroupBy(ar => ar.WorkType)
                .Select(ar => new Point
                {
                    Name = ar.Key.ToString(),
                    Y = ar.Count()
                });
            Highcharts chart = new Highcharts("chart")
                 .InitChart(new Chart { PlotShadow = false })
                 .SetTitle(new Title(){Text = "Produtividade"})
                 .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                 .SetPlotOptions(new PlotOptions
                 {
                     Pie = new PlotOptionsPie
                     {
                         AllowPointSelect = true,
                         Cursor = Cursors.Pointer,
                         DataLabels = new PlotOptionsPieDataLabels
                         {
                             Color = ColorTranslator.FromHtml("#333"),
                             ConnectorColor = ColorTranslator.FromHtml("#333"),
                             Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
                         }
                     }
                 })
                 .SetSeries(new Series
                 {
                     Type = ChartTypes.Pie,
                     Name = "Produtividade",
                     Data = new Data(new Point[]
                     {
                         new Point()
                         {
                             Color = Color.Red,
                             Name = "Não produtivo",
                             Y = 25
                         },
                         new Point()
                         {
                             Color = Color.DarkOrange,
                             Name = "Auxiliar",
                             Y = 45
                         },
                         new Point()
                         {
                             Color = Color.DarkGreen,
                             Name = "Produtivo",
                             Y = 30
                         }
                     })
                 });

            return PartialView(chart);
        }
    }
}