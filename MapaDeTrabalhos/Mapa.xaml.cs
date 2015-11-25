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
using Windows.UI.Core;
using Windows.Services.Maps;




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

        //Já inicia com a posição programada
        //protected async override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    // Specify a known location
        //    BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = -7.913488, Longitude = -34.913746 };
        //    Geopoint cityCenter = new Geopoint(cityPosition);

        //    // Set map location
        //    MapControl.Center = cityCenter;
        //    MapControl.ZoomLevel = 14;
        //    MapControl.LandmarksVisible = true;
        //}

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Specify a known location
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = -7.913488, Longitude = -34.913746 };
            Geopoint gp = new Geopoint(cityPosition);
            IAsyncOperation<MapLocationFinderResult> finder = MapLocationFinder.FindLocationsAsync("Rua oitenta e dois, Abreu e Lima, Pernambuco", gp);
            MapLocationFinderResult result = finder.GetResults();
            IReadOnlyList<MapLocation> mapLocations = result.Locations;
            MapLocation ml = mapLocations.ElementAt(0);
            double lat = ml.Point.Position.Latitude;
            double longitude = ml.Point.Position.Longitude;

            BasicGeoposition newPosition = new BasicGeoposition() { Latitude = lat, Longitude = longitude };
            Geopoint cityCenter = new Geopoint(newPosition);

            // Set map location
            MapControl.Center = cityCenter;
            MapControl.ZoomLevel = 14;
            MapControl.LandmarksVisible = true;
        }



    }
}