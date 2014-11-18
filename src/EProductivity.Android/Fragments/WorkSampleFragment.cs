using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using EProductivity.Droid.Helpers;
using EProductivity.Model;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.XamarinAndroid;

namespace EProductivity.Droid.Fragments
{
    public class WorkSampleFragment : Fragment
    {
        public event Action<Tour> NewTour = delegate { };
        public event Action<List<Tour>>  ViewTours = delegate { }; 

        private readonly WorkSample _workSample;
        private PlotView _protView;
        private TextView _titleTextView;
        private TextView _lastTourTextView;
        private Button _newTourButton;
        private Button _endWorkSampleButton;
        public WorkSampleFragment(WorkSample workSample)
        {
            _workSample = workSample;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.WorkSample, null, true);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _protView = View.FindViewById<PlotView>(Resource.Id.plotview);
            _titleTextView = View.FindViewById<TextView>(Resource.Id.title);
            _lastTourTextView = View.FindViewById<TextView>(Resource.Id.lastTour);
            _newTourButton = View.FindViewById<Button>(Resource.Id.newTour);
            _endWorkSampleButton = View.FindViewById<Button>(Resource.Id.endTour);

            if (_workSample.Status == WorkSampleStatus.Close)
            {
                _newTourButton.Visibility = ViewStates.Gone;
                _endWorkSampleButton.Visibility = ViewStates.Gone;
            }
            var pieSeries = new PieSeries()
            {
                Slices = new List<PieSlice>()
                {
                    new PieSlice("Produtivo",
                        _workSample.Tours.SelectMany(t => t.Observations)
                            .Count(o => o.Activity.Type == ActivityType.Work))
                    {
                        Fill = OxyColor.Parse("#77D065")
                    },
                    new PieSlice("Suplementar",
                        _workSample.Tours.SelectMany(t => t.Observations)
                            .Count(o => o.Activity.Type == ActivityType.Accessory))
                    {
                        Fill = OxyColor.Parse("#FFB508")
                    },
                    new PieSlice("Improdutivo",
                        _workSample.Tours.SelectMany(t => t.Observations)
                            .Count(o => o.Activity.Type == ActivityType.NotWork))
                    {
                        Fill = OxyColor.Parse("#E93A1A")
                    },
                }
            };
            _protView.Model = new PlotModel()
            {
                Title = "Produtividade",
                Background = OxyColor.Parse("#C4CCCC")
                
            };
            _protView.Model.Series.Add(pieSeries);

            _protView.Click += (sender, args) => ViewTours(_workSample.Tours);

            _newTourButton.Click += delegate
            {
                var tour = new Tour
                {
                    StartDate = DateTime.Now,
                    Observations = new List<Observation>(),
                    TourId = 1,
                    WorkSample = _workSample,
                    WorkSampleId = _workSample.WorkSampleId
                };
                _workSample.Tours.Add(tour);
                NewTour(tour);
            };
            _endWorkSampleButton.Click += delegate(object sender, EventArgs args)
            {
                _workSample.Status = WorkSampleStatus.Close;
                _newTourButton.Visibility = ViewStates.Gone;
                _endWorkSampleButton.Visibility = ViewStates.Gone;
            };

            _titleTextView.Text = _workSample.Title;
            var lastOrDefault = _workSample.Tours.Where(t => t.StartDate.HasValue).OrderBy(t => t.StartDate).LastOrDefault();
            _lastTourTextView.Text = string.Format("Ultimo tour: {0}", lastOrDefault != null ? lastOrDefault.StartDate.Value.ToShortDateString() : "Sem tours");
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
    }
}