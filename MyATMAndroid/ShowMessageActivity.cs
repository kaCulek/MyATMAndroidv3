using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Interop;

namespace MyATMAndroid
{
    [Activity(Label = "ShowMessageActivity")]
    public class ShowMessageActivity : Activity
    {
        private string accountNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.show_message);
            accountNumber = Intent.GetStringExtra("AccountNumber") ?? string.Empty;
            var message = Intent.GetStringExtra("Message") ?? string.Empty;
            var messageTextLabel = FindViewById<TextView>(Resource.Id.showMessageLabel);
            messageTextLabel.Text = message;
        }

        [Export("BackClick")]
        public void BackClick(Android.Views.View e)
        {
            var activity = new Intent(this, typeof(TransferActivity));
            activity.PutExtra("AccountNumber", accountNumber);
            StartActivity(activity);
        }
    }
}