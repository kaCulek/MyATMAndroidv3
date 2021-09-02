using Android.App;
using Android.Content;
using Android.OS;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : Activity
    {
        private string accountNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dashboard);
            accountNumber = Intent.GetStringExtra("AccountNumber") ?? string.Empty;
        }

        [Export("TransferAmountClick")]
        public void TransferAmountClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(TransferActivity));
            activity.PutExtra("AccountNumber", accountNumber);
            StartActivity(activity);
        }

        [Export("ShowCurrentBalance")]
        public void ShowCurrentBalance(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(CurrentBalanceActivity));
            activity.PutExtra("AccountNumber", accountNumber);
            StartActivity(activity);
        }

        [Export("LastTransactionsClick")]
        public void LastTransactionsClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(LastTransactionsActivity));
            activity.PutExtra("AccountNumber", accountNumber);
            StartActivity(activity);
        }

        [Export("LogoutClick")]
        public void LogoutClick(Android.Views.View e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}