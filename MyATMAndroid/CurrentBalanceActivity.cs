using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "CurrentBalanceActivity")]
    public class CurrentBalanceActivity : Activity
    {
        private string accountNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.current_balance);
            accountNumber = Intent.GetStringExtra("AccountNumber") ?? string.Empty;
            var users = new AtmUsers();
            var user = users.GetUser(accountNumber);
            var balanceLabel = FindViewById<TextView>(Resource.Id.currentBalanceLabel);
            var currentBalance = user.GetCurrentBalance();
            balanceLabel.Text = $"Current balance: {currentBalance}";
        }

        [Export("BackClick")]
        public void BackClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(DashboardActivity));
            activity.PutExtra("AccountNumber", accountNumber);
            StartActivity(activity);
        }
    }
}