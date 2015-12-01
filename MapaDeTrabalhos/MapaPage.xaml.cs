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
using MapaDeTrabalhos.viewModel;
using MapaDeTrabalhos.Model;
using Microsoft.WindowsAzure.MobileServices;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapaDeTrabalhos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapaPage : Page
    {

        private IMobileServiceTable<Endereco> EnderecoTable = App.MobileService.GetTable<Endereco>();
        private IMobileServiceTable<Anuncio> AnuncioTable = App.MobileService.GetTable<Anuncio>();
        private PontosNoMapaManeger poiManager;
        private Pessoa pessoa;
        private Endereco endereco;

        public MapaPage()
        {
            this.InitializeComponent();
            poiManager = new PontosNoMapaManeger();
            endereco = new Endereco();
            pessoa = new Pessoa();

            if (!pessoa.isPessoaFisica)
            {
                Sp_Currículo.Visibility = Visibility.Collapsed;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            pessoa = (Pessoa)e.Parameter;


            var itens = await EnderecoTable.ToCollectionAsync<Endereco>();
            List<Endereco> enderecos = itens.ToList();
            foreach (Endereco end in enderecos)
            {
                if (pessoa.Id.Equals(end.PessoaId))
                {
                    endereco = end;
                }
            }



            // Specify the location address
            string addressToGeocode = endereco.Rua + ", " + endereco.numero + ", " + endereco.bairro + ", " + endereco.cidade + " - " + endereco.estado;

            // Nearby location to use as a query hint.
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = -8.05665;
            queryHint.Longitude = -34.898441;
            Geopoint hintPoint = new Geopoint(queryHint);

            // Geocode the specified address, using the specified reference point
            // as a query hint. Return no more than 3 results.
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);

            //Setting position default to Derby - Recife
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = -8.05665, Longitude = -34.898441 };
            Geopoint cityCenter = new Geopoint(cityPosition);
            // If the query returns results, display the coordinates
            // of the first result.
            if (result.Status == MapLocationFinderStatus.Success)
            {
                cityPosition = new BasicGeoposition() { Latitude = result.Locations[0].Point.Position.Latitude, Longitude = result.Locations[0].Point.Position.Longitude };
                cityCenter = new Geopoint(cityPosition);
            }

            // Set map location
            MapControl.Center = cityCenter;
            MapControl.ZoomLevel = 14;
            MapControl.LandmarksVisible = true;

            var itens2 = await AnuncioTable.ToCollectionAsync<Anuncio>();
            List<Anuncio> anuncios = itens2.ToList();
            //Marcando os pontos no mapa

            MapItems.ItemsSource = poiManager.ListarAnuncios(anuncios);
        }

        private async void mapItemButton_Click(object sender, RoutedEventArgs e)
        {

            var buttonSender = sender as Button;
            PontoNoMapa p = (PontoNoMapa)buttonSender.DataContext;
            Anuncio a = p.anuncio;
            Tb_TituloAnuncio.Text = a.titulo;
            Tb_DescricaoAnuncio.Text = "Descrição: " + a.descricao;
            Tb_RequesitosAnuncio.Text = "Requesitos: " + a.exigencias;
            Tb_ValorAnuncio.Text = "Valor: " + a.valorOuSalario + " Reais.";

            var result = await AnuncioDialog.ShowAsync();
            string resultado = "" + result;
            if (resultado.Equals("Primary"))
            {
                //fazer o tratamento para quando o cara quiser indicar-se a vaga.
            }

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void AnuncioBotao_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AnuncioPage), pessoa);
        }

        private void CurriculoBotao_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CurriculoPage), pessoa);
        }

        private async void FiltroBotao_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await FiltroDialog.ShowAsync();
        }

        private async void Cb_areas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();

            var itens = await AnuncioTable.ToCollectionAsync<Anuncio>();
            List<Anuncio> anuncios = itens.ToList();

            if (valorSelecionado.Equals("Todas as áreas"))
            {

                MapItems.ItemsSource = poiManager.ListarAnuncios(anuncios);
            }
            else
            {
                MapItems.ItemsSource = poiManager.ListarAnunciosFiltro(valorSelecionado, anuncios);
            }
        }

        private async void TipoTrabalhoButao_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await TipoDialog.ShowAsync();

            var itens = await AnuncioTable.ToCollectionAsync<Anuncio>();
            List<Anuncio> anuncios = itens.ToList();

            if (Rb_Todos.IsChecked == true)
            {
                MapItems.ItemsSource = poiManager.ListarAnuncios(anuncios);
            }
            else if (Rb_Formal.IsChecked == true)
            {
                MapItems.ItemsSource = poiManager.ListarAnunciosFormal(anuncios);
            }
            else
            {
                MapItems.ItemsSource = poiManager.ListarAnunciosInformal(anuncios);
            }
        }


        private void FinalizarAnuncioBotao_Click(object sender, RoutedEventArgs e)
        {
            //Fazer a função pra mostra na tela a lista dos anuncios do cara pra depois direcionar pra tela de Anuncios
        }


        private void MeuCadastroBotao_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(CadastroPage), pessoa);
        }

        private void CurriculoBotao_Click(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(CurriculoPage), pessoa);
        }

        private void LogoutBotao_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}