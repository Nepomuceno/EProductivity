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
using EProductivity.Droid.Helpers;
using EProductivity.Model;
using Activity = Android.App.Activity;

namespace EProductivity.Droid.Fragments
{
    public class WorkersFragment : ListFragment
    {
        private readonly List<Worker> _workers;

        public WorkersFragment(List<Worker> workers)
        {
            _workers = workers;
        }

        public event Action<Worker, int> WorkerSelected = delegate { };
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
            ListAdapter = new WorkerAdapter(view.Context, _workers);
        }
        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            WorkerSelected(_workers[position], v.Top);
        }
    }

    public class WorkerAdapter : BaseAdapter
    {
        private readonly Context _context;
        List<Worker> _workers;

        public WorkerAdapter(Context context, List<Worker> workers)
        {
            _context = context;
            _workers = workers;
        }


        public override int Count
        {
            get { return _workers.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _workers[position].WorkerId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                var inflater = LayoutInflater.FromContext(_context);
                convertView = inflater.Inflate(Resource.Layout.WorkerListItem, parent, false);
                convertView.Id = 0x60000000;
            }
            convertView.Id++;
            var name = convertView.FindViewById<TextView>(Resource.Id.workerName);
            var area = convertView.FindViewById<TextView>(Resource.Id.workerArea);
            var worker = _workers[position];
            name.Text = worker.Name;
            area.Text = worker.Area.Name;
            var image = convertView.FindViewById<ImageView>(Resource.Id.workerImage);
            image.SetImageFromUrlAsync(worker.PictureUrl).Wait(TimeSpan.FromSeconds(10));
            return convertView;
        }
    }
}