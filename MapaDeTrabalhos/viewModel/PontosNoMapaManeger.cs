using MapaDeTrabalhos.Model;
using Microsoft.WindowsAzure.MobileServices;
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
        public List<PontoNoMapa> ListarAnuncios(List<Anuncio> anuncios)
        {
            List<PontoNoMapa> pois = new List<PontoNoMapa>();
            foreach (Anuncio anuncio in anuncios)
            {

                if (anuncio.isAberto)
                {
                    Uri icon;
                    if (anuncio.isFormal)
                    {
                        icon = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute);
                    }
                    else
                    {
                        icon = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute);
                    }
                    pois.Add(new PontoNoMapa()
                    {

                        DisplayName = anuncio.titulo,
                        ImageSourceUri = icon,
                        anuncio = anuncio,
                        Location = new Geopoint(new BasicGeoposition()
                        {
                            Latitude = anuncio.latitude,
                            Longitude = anuncio.longitude
                        })
                    });
                }
            }
            return pois;
        }

        public List<PontoNoMapa> ListarAnunciosFiltro(string filtro, List<Anuncio> anuncios)
        {
            List<PontoNoMapa> pois = new List<PontoNoMapa>();
            foreach (Anuncio anuncio in anuncios)
            {
                if (anuncio.area != null)
                {
                    if (anuncio.isAberto)
                    {
                        if (anuncio.area.Equals(filtro))
                        {
                            Uri icon;
                            if (anuncio.isFormal)
                            {
                                icon = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute);
                            }
                            else
                            {
                                icon = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute);
                            }
                            pois.Add(new PontoNoMapa()
                            {

                                DisplayName = anuncio.titulo,
                                ImageSourceUri = icon,
                                anuncio = anuncio,
                                Location = new Geopoint(new BasicGeoposition()
                                {
                                    Latitude = anuncio.latitude,
                                    Longitude = anuncio.longitude
                                })
                            });
                        }
                    }
                }
            }
            return pois;
        }

        public List<PontoNoMapa> ListarAnunciosFormal(List<Anuncio> anuncios)
        {
            List<PontoNoMapa> pois = new List<PontoNoMapa>();
            foreach (Anuncio anuncio in anuncios)
            {

                if (anuncio.isAberto)
                {
                    if (anuncio.isFormal)
                    {
                        pois.Add(new PontoNoMapa()
                        {
                            DisplayName = anuncio.titulo,
                            ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation.png", UriKind.RelativeOrAbsolute),
                            anuncio = anuncio,
                            Location = new Geopoint(new BasicGeoposition()
                            {
                                Latitude = anuncio.latitude,
                                Longitude = anuncio.longitude
                            })
                        });
                    }

                }
            }
            return pois;
        }

        public List<PontoNoMapa> ListarAnunciosInformal(List<Anuncio> anuncios)
        {
            List<PontoNoMapa> pois = new List<PontoNoMapa>();
            foreach (Anuncio anuncio in anuncios)
            {

                if (anuncio.isAberto)
                {
                    if (!anuncio.isFormal)
                    {
                        pois.Add(new PontoNoMapa()
                        {
                            DisplayName = anuncio.titulo,
                            ImageSourceUri = new Uri("ms-appx:///Assets/MapMarcation2.png", UriKind.RelativeOrAbsolute),
                            anuncio = anuncio,
                            Location = new Geopoint(new BasicGeoposition()
                            {
                                Latitude = anuncio.latitude,
                                Longitude = anuncio.longitude
                            })
                        });
                    }

                }
            }
            return pois;
        }

    }

}

