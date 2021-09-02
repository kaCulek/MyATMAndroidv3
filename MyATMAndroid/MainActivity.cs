using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var cardInput = FindViewById<EditText>(Resource.Id.cardInput);
            cardInput.Text = "HR230000000000000001";
            var pinInput = FindViewById<EditText>(Resource.Id.pinInput);
            pinInput.Text = "1234";
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, 
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Export("LoginClick")]
        public void LoginClick(Android.Views.View e)
        {
            var cardInput = FindViewById<EditText>(Resource.Id.cardInput);
            var pinInput = FindViewById<EditText>(Resource.Id.pinInput);
            var card = cardInput.Text.ToString();
            var pin = pinInput.Text.ToString();
            var users = new AtmUsers();
            var user = users.GetUser(card);
            if (user != null && user.Pin == pin)
            {
                var activity = new Intent(this, typeof(DashboardActivity));
                activity.PutExtra("AccountNumber", user.AccountNumber);
                StartActivity(activity);
            }
        }
    }
}