using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyATMAndroid
{
    public class LastTransactionsAdapter : BaseAdapter
    {
        private Context c;
        private JavaList<AtmUserTransaction> transactions;
        private LayoutInflater inflater;
        public LastTransactionsAdapter(JavaList<AtmUserTransaction> money, Context c)
        {
            this.transactions = money;
            this.c = c;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return transactions.Get(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (inflater == null)
            {
                inflater = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);
            }
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.transaction, parent, false);
            }
            //BIND DATA
            var amount = convertView.FindViewById<TextView>(Resource.Id.transactionAmount);
            var currentBalance = convertView.FindViewById<TextView>(Resource.Id.transactionCurrentBalance);
            var transactionDate = convertView.FindViewById<TextView>(Resource.Id.transactionDate);
            var description = convertView.FindViewById<TextView>(Resource.Id.description);
            var tran = transactions[position];
            amount.Text = tran.Amount.ToString();
            currentBalance.Text = tran.AccountBalance.ToString();
            transactionDate.Text = tran.Date.ToShortDateString();
            description.Text = tran.Description;
            return convertView;
        }
        public override int Count
        {
            get { return transactions.Size(); }
        }
    }
}