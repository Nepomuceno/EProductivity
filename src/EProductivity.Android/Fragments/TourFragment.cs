using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using EProductivity.Droid.Helpers;
using Activity = EProductivity.Model.Activity;
using EProductivity.Model;

namespace EProductivity.Droid.Fragments
{
    public class TourFragment : Fragment
    {
        private readonly Tour _tour;
        private List<Activity> _activities;
        private int _position = 0;

        private Button _workingButton;
        private Button _acessoryButton;
        private Button _notWorkingButton;

        public event Action<Tour> TourFinished = delegate { };

        public TourFragment(Tour tour)
        {
            _tour = tour;
            _activities = new List<Activity>
            {
                new Activity()
                {
                    Description = "Trabalhando",
                    Type = ActivityType.Work
                },
                new Activity()
                {
                    Description = "Transportando material",
                    Type = ActivityType.Accessory
                },
                new Activity()
                {
                    Description = "Manutenção no equipamento",
                    Type = ActivityType.Accessory
                },
                new Activity()
                {
                    Description = "Ausente",
                    Type = ActivityType.NotWork
                },
                new Activity()
                {
                    Description = "No banheiro",
                    Type = ActivityType.NotWork
                },
                new Activity()
                {
                    Description = "Não Trabalho",
                    Type = ActivityType.NotWork
                }
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Tour, null, true);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _workingButton = View.FindViewById<Button>(Resource.Id.working);
            _acessoryButton = View.FindViewById<Button>(Resource.Id.accessory);
            _notWorkingButton = View.FindViewById<Button>(Resource.Id.notWorking);
            _workingButton.Click += async (sender, e) =>
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(view.Context);
                dialog.SetTitle("Selecione a atividade:");
                var workingActivities = _activities.Where(a => a.Type == ActivityType.Work).ToList();
                dialog.SetItems(workingActivities.Select(a => a.Description).ToArray(),
                    (o, args) =>
                    {
                        var observation = new Observation()
                        {
                            Activity = workingActivities[args.Which],
                            Date = DateTime.Now,
                            ObservationId = _position,
                            Tour = _tour,
                            Worker = _tour.WorkSample.Workers[_position]
                        };
                        _tour.Observations.Add(observation);
                        _position++;
                        if (_position < _tour.WorkSample.Workers.Count)
                        {
                            LoadWorker(_tour.WorkSample.Workers[_position], view);
                        }
                        else
                        {
                            _tour.EndDate = DateTime.Now;
                            TourFinished(_tour);
                        }
                    });
                var alerDialog = dialog.Create();
                alerDialog.Window.ClearFlags(WindowManagerFlags.DimBehind);
                alerDialog.Show();
            };
            _acessoryButton.Click += async (sender, e) =>
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(view.Context);
                dialog.SetTitle("Selecione a atividade:");
                var workingActivities = _activities.Where(a => a.Type == ActivityType.Accessory).ToList();
                dialog.SetItems(workingActivities.Select(a => a.Description).ToArray(),
                    (o, args) =>
                    {
                        var observation = new Observation()
                        {
                            Activity = workingActivities[args.Which],
                            Date = DateTime.Now,
                            ObservationId = _position,
                            Tour = _tour,
                            Worker = _tour.WorkSample.Workers[_position]
                        };
                        _tour.Observations.Add(observation);
                        _position++;
                        if (_position < _tour.WorkSample.Workers.Count)
                        {
                            LoadWorker(_tour.WorkSample.Workers[_position], view);
                        }
                        else
                        {
                            _tour.EndDate = DateTime.Now;
                            TourFinished(_tour);
                        }
                    });
                var alerDialog = dialog.Create();
                alerDialog.Window.ClearFlags(WindowManagerFlags.DimBehind);
                alerDialog.Show();
            };

            _notWorkingButton.Click += async (sender, e) =>
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(view.Context);
                dialog.SetTitle("Selecione a atividade:");
                var workingActivities = _activities.Where(a => a.Type == ActivityType.NotWork).ToList();
                dialog.SetItems(workingActivities.Select(a => a.Description).ToArray(),
                    (o, args) =>
                    {
                        var observation = new Observation()
                        {
                            Activity = workingActivities[args.Which],
                            Date = DateTime.Now,
                            ObservationId = _position,
                            Tour = _tour,
                            Worker = _tour.WorkSample.Workers[_position]
                        };
                        _tour.Observations.Add(observation);
                        _position++;
                        if (_position < _tour.WorkSample.Workers.Count)
                        {
                            LoadWorker(_tour.WorkSample.Workers[_position], view);
                        }
                        else
                        {
                            _tour.EndDate = DateTime.Now;
                            TourFinished(_tour);
                        }
                    });
                var alerDialog = dialog.Create();
                alerDialog.Window.ClearFlags(WindowManagerFlags.DimBehind);
                alerDialog.Show();
            };
            if (_position >= _tour.WorkSample.Workers.Count)
            {
                _position = 0;
            }
            LoadWorker(_tour.WorkSample.Workers[_position], view);
        }


        public void LoadWorker(Worker worker, View view)
        {
            var name = view.FindViewById<TextView>(Resource.Id.nameWorker);
            name.Text = worker.Name;
            var image = view.FindViewById<ImageView>(Resource.Id.workerImage);
            image.SetImageFromUrlAsync(worker.PictureUrl);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }


    }
}