using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EProductivity.Droid.Helpers;
using EProductivity.Droid.Model;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.XamarinAndroid;
using Activity = EProductivity.Droid.Model.Activity;

namespace EProductivity.Droid.Fragments
{
    public class ToursFragment : ListFragment
    {
        private readonly List<Tour> _tours;

        public ToursFragment(List<Tour> tours)
        {
            _tours = tours;
        }

        public event Action<Tour, int> TourSelected = delegate { };
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.WorkSamples, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {

            base.OnViewCreated(view, savedInstanceState);
            ListView.SetDrawSelectorOnTop(true);
            ListAdapter = new ToursAdapter(view.Context, _tours);
        }
        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            TourSelected(_tours[position], v.Top);
        }

    }
            public class ToursAdapter : BaseAdapter
        {
            private readonly Context _context;
            List<Tour> _tours;

            public ToursAdapter(Context context, List<Tour> tours)
            {
                _context = context;
                _tours = tours;
            }


            public override int Count
            {
                get { return _tours.Count; }
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }

            public override long GetItemId(int position)
            {
                return _tours[position].TourId;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    var inflater = LayoutInflater.FromContext(_context);
                    convertView = inflater.Inflate(Resource.Layout.TourListItem, parent, false);
                    convertView.Id = 0x60000000;
                }
                convertView.Id++;
                var protView = convertView.FindViewById<PlotView>(Resource.Id.tourPlotResult);

                var title = convertView.FindViewById<TextView>(Resource.Id.tourTitle);
                var produtividade = convertView.FindViewById<TextView>(Resource.Id.tourProdutividade);
                var tour = _tours[position];


                var pieSeries = new PieSeries()
                {
                    Slices = new List<PieSlice>()
                {
                    new PieSlice("Produtivo",
                        tour.Observations
                            .Count(o => o.Activity.Type == ActivityType.Work))
                    {
                        Fill = OxyColor.Parse("#77D065")
                    },
                    new PieSlice("Suplementar",
                        tour.Observations
                            .Count(o => o.Activity.Type == ActivityType.Accessory))
                    {
                        Fill = OxyColor.Parse("#FFB508")
                    },
                    new PieSlice("Improdutivo",
                        tour.Observations
                            .Count(o => o.Activity.Type == ActivityType.NotWork))
                    {
                        Fill = OxyColor.Parse("#E93A1A")
                    },
                }
                
                
                };
                protView.Model = new PlotModel()
                {
                    Background = OxyColor.Parse("#C4CCCC"),
                    IsLegendVisible = false
                };
                protView.Model.Series.Add(pieSeries);

                title.Text = "Tour em : " + tour.StartDate.Value.ToString("g", new CultureInfo("pt-BR"));

                produtividade.Text = "Produtividade : " + ((tour.Observations.Count(o => o.Activity.Type == ActivityType.Work) / (float)tour.Observations.Count)).ToString("P");
                return convertView;
            }
        }

}