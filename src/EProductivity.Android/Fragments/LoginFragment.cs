using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace EProductivity.Droid.Fragments
{
    public class LoginFragment : Fragment
    {
        EditText password;
        Button login;
        ImageView imageView;

        public event Action LoginSucceeded = delegate { };
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Login, container, false);

            imageView = view.FindViewById<ImageView>(Resource.Id.imageView1);
            LoadUserImage();

            var textView = view.FindViewById<EditText>(Resource.Id.email);
            textView.Enabled = true;

            password = view.FindViewById<EditText>(Resource.Id.password);
            login = view.FindViewById<Button>(Resource.Id.signInBtn);
            login.Click += (sender, e) => Login(textView.Text, password.Text);
            return view;
        }

        private void Login(string user, string password)
        {
            if (user == password)
            {
                LoginSucceeded();
            }
            else
            {
                Toast.MakeText(this.Activity, "Favor colocar um usuario e senha corretos", ToastLength.Long).Show();
            }
            
        }

        private void LoadUserImage()
        {
            imageView.SetImageResource(Resource.Drawable.logo_full);
        }
    }
}