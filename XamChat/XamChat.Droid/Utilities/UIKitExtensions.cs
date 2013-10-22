using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamChat.Droid
{
    public static class UIKitExtensions
    {
        public static void DisplayError(this Exception exc, Context context)
        {
            string error;
            AggregateException aggregate = exc as AggregateException;
            if (aggregate != null)
            {
                error = aggregate.InnerExceptions.First().Message;
            }
            else
            {
                error = exc.Message;
            }

            new AlertDialog.Builder(context)
                .SetTitle(Resource.String.ErrorTitle)
                .SetMessage(error)
                .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
                .Show();
        }
    }
}

