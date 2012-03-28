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
using Microsoft;
using Microsoft.Phone;
using Microsoft.Phone.Shell;
using System.Linq;

namespace SampleShared
{
    public static class TileNotifications
    {
        public static void UpdateTile(String title, String backgroundImageUri, int count)
        {
            ShellTile firstTile = ShellTile.ActiveTiles.First();
            var newData = new StandardTileData() 
            { 
                Title = title, 
                BackgroundImage = new Uri(backgroundImageUri, UriKind.Relative),
                Count = count, 
            };

            firstTile.Update(newData);
        }
    }
}
