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

                DisplayName = "Programador Java Junior",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude + 0.005,
                    Longitude = center.Longitude - 0.005
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Programador Java Pleno",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude + 0.010,
                    Longitude = center.Longitude + 0.005
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Programador C# Junior",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.018,
                    Longitude = center.Longitude - 0.008
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Programador C# Senior",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.008,
                    Longitude = center.Longitude + 0.009
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Programador C# Senior",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.008,
                    Longitude = center.Longitude + 0.009
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Suporte Técnico",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.010,
                    Longitude = center.Longitude + 0.019
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Analista de Sistemas",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.020,
                    Longitude = center.Longitude + 0.019
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Operador de Telemarketing",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Latitude - 0.002,
                    Longitude = center.Longitude + 0.019
                })
            });
            pois.Add(new PontoNoMapa()
            {
                DisplayName = "Gerente de Sistemas",
                ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute),
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

