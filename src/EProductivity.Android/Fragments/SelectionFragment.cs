using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace EProductivity.Droid.Fragments
{
    public class SelectionFragment : Fragment
    {
        Button workerButton;
        Button workSampleButton;

        public event Action WorkerSelected = delegate { };
        public event Action WorkSampleSelected = delegate { };
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.SelectionPage, container, false);

            workerButton = view.FindViewById<Button>(Resource.Id.btnTrabalhadores);
            workSampleButton = view.FindViewById<Button>(Resource.Id.btnAmostragens);

            workerButton.Click += (sender, e) => WorkerSelected();
            workSampleButton.Click += (sender, e) => WorkSampleSelected();

            return view;
        }

        

    }
}