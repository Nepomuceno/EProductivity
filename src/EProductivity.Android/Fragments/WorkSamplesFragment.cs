using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Activity = Android.App.Activity;
using EProductivity.Model;

namespace EProductivity.Droid.Fragments
{
    public class WorkSamplesFragment : ListFragment
    {
        private readonly List<WorkSample> _workSamples;

        public WorkSamplesFragment(List<WorkSample> workSamples)
        {
            _workSamples = workSamples;
        }

        public event Action<WorkSample, int> WorkSampleSelected = delegate { };
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
            ListAdapter = new WorkSampleAdapter(view.Context,_workSamples);
        }
        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            WorkSampleSelected(_workSamples[position], v.Top);
        }
    }

    public class WorkSampleAdapter : BaseAdapter
    {
        private readonly Context _context;
        List<WorkSample> _workSamples;

        public WorkSampleAdapter(Context context, List<WorkSample> workSamples)
        {
            _context = context;
            _workSamples = workSamples;
        }


        public override int Count
        {
            get { return _workSamples.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _workSamples[position].WorkSampleId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                var inflater = LayoutInflater.FromContext(_context);
                convertView = inflater.Inflate(Resource.Layout.WorkSampleListItem, parent, false);
                convertView.Id = 0x60000000;
            }
            convertView.Id++;
            var titleTextView = convertView.FindViewById<TextView>(Resource.Id.title);
            var lastTourTextView = convertView.FindViewById<TextView>(Resource.Id.lastTour);
            var workSample = _workSamples[position];
            if (workSample.Status == WorkSampleStatus.Close)
            {
                titleTextView.SetTextColor(new Color(Resource.Color.xam_gray));
            }
            titleTextView.Text = workSample.Title;
            var lastOrDefault = workSample.Tours.Where(t => t.StartDate.HasValue).OrderBy(t => t.StartDate).LastOrDefault();
            lastTourTextView.Text = string.Format("Ultimo tour: {0}", lastOrDefault != null ? lastOrDefault.StartDate.Value.ToString("g",new CultureInfo("pt-BR")) : "Sem tours");
            return convertView;
        }
    }
}