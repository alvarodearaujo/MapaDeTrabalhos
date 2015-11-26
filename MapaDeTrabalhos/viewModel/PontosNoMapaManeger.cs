using MapaDeTrabalhos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Maps;

namespace MapaDeTrabalhos.viewModel
{
    class PontosNoMapaManeger
    {
        public List<PontoNoMapa> FetchPOIs(BasicGeoposition center)
        {
            List<PontoNoMapa> pois = new List<PontoNoMapa>();
            pois.Add(new PontoNoMapa()
            {

                DisplayName = "Titulo da vaga 1",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude + 0.001,
                    Longitude = center.Longitude - 0.001
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Titulo da vaga Two",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude + 0.001,
                    Longitude = center.Longitude + 0.001
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Titulo da vaga  Three",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.001,
                    Longitude = center.Longitude - 0.001
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Titulo da vaga  Four",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.001,
                    Longitude = center.Longitude + 0.001
                })
            });
            return pois;
        }

    }
}

