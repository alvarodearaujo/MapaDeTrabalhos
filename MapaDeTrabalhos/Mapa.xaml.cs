using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapaDeTrabalhos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Mapa : Page
    {

        public Mapa()
        {
            this.InitializeComponent();
            
        }

        //protected async override void OnNavigatedTo(NavigationEventArgs e)
        //{

        //    var accessStatus = await Geolocator.RequestAccessAsync();
        //    switch (accessStatus)
        //    {
        //        case GeolocationAccessStatus.Allowed:

        //            // Get the current location
        //            Geolocator geolocator = new Geolocator();
        //            Geoposition pos = await geolocator.GetGeopositionAsync();
        //            Geopoint myLocation = pos.Coordinate.Point;

        //            // Set map location
        //            MapControl1.Center = myLocation;
        //            MapControl1.ZoomLevel = 12;
        //            MapControl1.LandmarksVisible = true;
        //            break;

        //        case GeolocationAccessStatus.Denied:
        //            // Handle when access to location is denied
        //            break;

        //        case GeolocationAccessStatus.Unspecified:
        //            // Handle when an unspecified error occurs
        //            break;
        //    }
        //}
    }
}
