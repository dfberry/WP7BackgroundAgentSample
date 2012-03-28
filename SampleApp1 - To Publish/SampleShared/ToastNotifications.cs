using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;

namespace SampleShared
{
    public static class ToastNotifications
    {
        public static void ShowToast(String title, String message, String NavigationURL)
        {
            var toast = new ShellToast
            {
                Title = title,
                Content = message,
                NavigationUri = new System.Uri(NavigationURL, System.UriKind.Relative)
            }; 
            
            toast.Show();
        }
    }
}
