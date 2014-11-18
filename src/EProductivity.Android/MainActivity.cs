using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using EProductivity.Droid.Fragments;
using EProductivity.Droid.Helpers;
using EProductivity.Model;
using Activity = Android.App.Activity;
using System.Collections.Generic;

namespace EProductivity.Droid
{
    [Activity(Label = "E-Productivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int baseFragment;
        private List<WorkSample> _workSamples;
        private List<Worker> _workers; 

        protected override void OnCreate(Bundle bundle)
        {
            var metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            Images.ScreenWidth = metrics.WidthPixels;
            FileCache.SaveLocation = CacheDir.AbsolutePath;
            base.OnCreate(bundle);
            _workSamples = SeedClass.GetSamples();
            _workers = SeedClass.GetWorkers();
            SetContentView(Resource.Layout.Main);
            

            //Retain fragments so don't set home if state is stored.
            if (FragmentManager.BackStackEntryCount == 0)
            {
                var loginVc = new LoginFragment();
                loginVc.LoginSucceeded += ShowSelection;
                baseFragment = loginVc.Id;
                SwitchScreens(loginVc, false, true);

            }
        }



        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("baseFragment", baseFragment);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            baseFragment = savedInstanceState.GetInt("baseFragment");
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                //case Resource.Id.Home:
                //    //pop full backstack when going home.	
                //    FragmentManager.PopBackStack(baseFragment, PopBackStackFlags.Inclusive);
                //    SetupActionBar();
                //    return true;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            SetupActionBar(FragmentManager.BackStackEntryCount != 0);
        }

        public int SwitchScreens(Fragment fragment, bool animated = true, bool isRoot = false)
        {
            var transaction = FragmentManager.BeginTransaction();

            if (animated)
            {
                int animIn, animOut;
                GetAnimationsForFragment(fragment, out animIn, out animOut);
                transaction.SetCustomAnimations(animIn, animOut);
            }
            transaction.Replace(Resource.Id.contentArea, fragment);
            if (!isRoot)
                transaction.AddToBackStack(null);

            SetupActionBar(!isRoot);

            return transaction.Commit();
        }

        void GetAnimationsForFragment(Fragment fragment, out int animIn, out int animOut)
        {
            animIn = Resource.Animation.enter;
            animOut = Resource.Animation.exit;

            switch (fragment.GetType().Name)
            {
                case "TourFragment":
                    animIn = Resource.Animation.product_detail_in;
                    animOut = Resource.Animation.product_detail_out;
                    break;
            }
        }

        public void ShowWorkSampleDetail(WorkSample workSample, int itemVerticalOffset)
        {
            var workSampleFragment = new WorkSampleFragment(workSample);
            workSampleFragment.NewTour += WorkSampleFragmentOnNewTour;
            workSampleFragment.ViewTours += ViewTours;
            SwitchScreens(workSampleFragment);
        }

        private void WorkSampleFragmentOnNewTour(Tour tour)
        {
            var tourFragment = new TourFragment(tour);
            tourFragment.TourFinished += TourFinished;
            SwitchScreens(tourFragment);
        }

        private void ViewTours(List<Tour> tours)
        {
            var toursFragment = new ToursFragment(tours);
            SwitchScreens(toursFragment);
        }

        private void TourFinished(Tour tour)
        {
            var workSampleFragment = new WorkSamplesFragment(_workSamples);
            workSampleFragment.WorkSampleSelected += ShowWorkSampleDetail;
            SwitchScreens(workSampleFragment);
        }

        /// <summary>
        /// Setups the action bar if we want to show up arrow or not
        /// </summary>
        /// <param name="showUp">If set to <c>true</c> show up.</param>
        public void SetupActionBar(bool showUp = false)
        {
            this.ActionBar.SetDisplayHomeAsUpEnabled(showUp);
            //this.ActionBar.SetDisplayShowHomeEnabled (showUp);
        }

        public void ShowLogin()
        {
            var loginVc = new LoginFragment();
            loginVc.LoginSucceeded += ShowSelection;
            SwitchScreens(loginVc);
        }

        public void ShowWorkSamples()
        {
            var workSampleFragment = new WorkSamplesFragment(_workSamples);
            workSampleFragment.WorkSampleSelected += ShowWorkSampleDetail;
            SwitchScreens(workSampleFragment);
        }

        public void ShowWorkers()
        {
            var workerFragment = new WorkersFragment(_workers);
            workerFragment.WorkerSelected += ShowWorkerDetail;
            SwitchScreens(workerFragment);
        }

        private void ShowWorkerDetail(Worker worker, int position)
        {
            
        }

        public void ShowSelection()
        {
            var selectionFragment = new SelectionFragment();
            selectionFragment.WorkerSelected += ShowWorkers;
            selectionFragment.WorkSampleSelected += ShowWorkSamples;
            SwitchScreens(selectionFragment);
        }




    }

    public static class SeedClass
    {
        public static List<Worker> GetWorkers()
        {
            var officeArea = new Area()
            {
                AreaId = 1,
                Name = "Escritorio"
            };
            var productionArea = new Area()
            {
                AreaId = 2,
                Name = "Produção"
            };

            var workers = new List<Worker>()
            {
                new Worker()
                {
                    Name = "Joana",
                    PictureUrl = "http://www.ktmrecruitment.co.uk/images/office_worker3.jpg",
                    AreaId = officeArea.AreaId,
                    Area = officeArea
                },
                new Worker()
                {
                    Name = "Julia",
                    PictureUrl = "http://www.aptmags.com/wp-content/uploads/2013/01/answeringphone.jpg",
                    AreaId = officeArea.AreaId,
                    Area = officeArea
                },
                new Worker()
                {
                    Name = "Fernando",
                    PictureUrl = "https://thecubiclerebel.files.wordpress.com/2012/04/office_worker.jpg",
                    AreaId = officeArea.AreaId,
                    Area = officeArea
                },
                new Worker()
                {
                    Name = "Jose",
                    PictureUrl = "http://www.chronicle.su/wp-content/uploads/harold-strafford.jpg",
                    AreaId = officeArea.AreaId,
                    Area = officeArea
                },
                new Worker()
                {
                    Name = "Cicero",
                    PictureUrl = "http://ewic.org/wp-content/themes/ewic/images/Construction%20Worker.png",
                    Area = productionArea,
                    AreaId = productionArea.AreaId
                },
                new Worker()
                {
                    Name = "Claudio",
                    PictureUrl = "http://www.goodenoughmother.com/wp-content/uploads/2011/11/worker-wearing-hard-hat.jpg",
                    Area = productionArea,
                    AreaId = productionArea.AreaId
                },
                new Worker()
                {
                    Name = "Marcelo Conceição",
                    PictureUrl = "http://www.gbrs-uk.com/images/construction-worker.png",
                    Area = productionArea,
                    AreaId = productionArea.AreaId
                },
                new Worker()
                {
                    Name = "Marco",
                    PictureUrl = "http://www.u-tax.net/wp-content/uploads/2013/02/construction_worker.jpg",
                    Area = productionArea,
                    AreaId = productionArea.AreaId
                },
                new Worker()
                {
                    Name = "Jailton",
                    PictureUrl = "http://www.latuasicurezza.com/wp-content/uploads/2012/09/worker.jpg",
                    Area = productionArea,
                    AreaId = productionArea.AreaId
                }

            };
            return workers;
        }
        public static List<WorkSample> GetSamples()
        {
            var officeArea = new Area()
            {
                AreaId = 1,
                Name = "Escritorio"
            };
            var productionArea = new Area()
            {
                AreaId = 2,
                Name = "Produção"
            };
            var samples = new List<WorkSample>();
            var workSampleBase = new WorkSample()
            {
                Organization = new Organization()
                {
                    Document = "077.611.486-78",
                    Type = OrganizationType.Individual
                },
                OrganizationId = 1,
                Title = "Amostragem de campo",
                Tours = new List<Tour>(),
                Workers = GetWorkers().Where(w => w.AreaId == productionArea.AreaId).ToList(),
                WorkSampleId = 1
            };

            var workSampleBaseEscritorio = new WorkSample()
            {
                Organization = new Organization()
                {
                    Document = "077.611.486-78",
                    Type = OrganizationType.Individual
                },
                OrganizationId = 1,
                Title = "Amostragem de escritorio",
                Tours = new List<Tour>(),
                Workers = GetWorkers().Where(w => w.AreaId == officeArea.AreaId).ToList(),
                WorkSampleId = 1
            };

            samples.Add(workSampleBase);
            samples.Add(workSampleBaseEscritorio);
            return samples;
        }
    }
}

