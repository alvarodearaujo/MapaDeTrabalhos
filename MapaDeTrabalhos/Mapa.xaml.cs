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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Make the funcion to take the address of the user.


            // Specify the location address
            string addressToGeocode = "Rua oitenta e dois, caetés 3, abreu e lima - Pernambuco";

            // Nearby location to use as a query hint.
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = -8.05665;
            queryHint.Longitude = -34.898441;
            Geopoint hintPoint = new Geopoint(queryHint);

            // Geocode the specified address, using the specified reference point
            // as a query hint. Return no more than 3 results.
            MapLocationFinderResult result =   await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);

            //Setting position default to Derby - Recife
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = -8.05665, Longitude = -34.898441 };
            Geopoint cityCenter = new Geopoint(cityPosition);
            // If the query returns results, display the coordinates
            // of the first result.
            if (result.Status == MapLocationFinderStatus.Success)
            {
                cityPosition = new BasicGeoposition() { Latitude = result.Locations[0].Point.Position.Latitude, Longitude = result.Locations[0].Point.Position.Longitude};
                cityCenter = new Geopoint(cityPosition);
            }

            // Set map location
            MapControl.Center = cityCenter;
            MapControl.ZoomLevel = 14;
            MapControl.LandmarksVisible = true;
        }



    }
}