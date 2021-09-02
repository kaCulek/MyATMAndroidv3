using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "TransferActivity")]
    public class TransferActivity : Activity
    {
        private string accountNumber;
        private AtmUsers users;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.transfer);
            accountNumber = Intent.GetStringExtra("AccountNumber") ?? string.Empty;
            users = new AtmUsers();
            FindViewById<EditText>(Resource.Id.iban).FocusChange += (s, e) =>
            {
                bool hasFocus = e.HasFocus;
                if (!hasFocus)
                {
                    var targetAccountNumber = FindViewById<EditText>(Resource.Id.iban).Text;
                    if (!string.IsNullOrEmpty(targetAccountNumber))
                    {
                        var user = users.GetUser(targetAccountNumber);
                        if (user != null)
                        {
                            FindViewById<EditText>(Resource.Id.name).Text = $"{user.Name} {user.LastName}";
                        }
                    }
                }
            };
        }

        [Export("TransferClick")]
        public void TransferClick(Android.Views.View evt)
        {
            var targetAccountInput = FindViewById<EditText>(Resource.Id.iban);
            var amountInput = FindViewById<EditText>(Resource.Id.amount);
            var targetAccountNumber = targetAccountInput.Text.ToString();
            var amount = decimal.Parse(amountInput.Text.ToString());
            try
            {
                users.TransferAmount(this.accountNumber, targetAccountNumber, amount);
                OpenDashboard();
            }
            catch (TransactionException e)
            {
                var activity = new Intent(this, typeof(ShowMessageActivity));
                activity.PutExtra("AccountNumber", accountNumber);
                activity.PutExtra("Message", e.Message);
                StartActivity(activity);
            }
        }

        [Export("BackClick")]
        public void BackClick(Android.Views.View e)
        {
            OpenDashboard();
        }

        private void OpenDashboard()
        {
            var activity = new Intent(this, typeof(DashboardActivity));
            activity.PutExtra("AccountNumber", accountNumber);
            StartActivity(activity);
        }
    }
}