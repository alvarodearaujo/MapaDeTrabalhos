using MapaDeTrabalhos.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapaDeTrabalhos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnuncioPage : Page
    {

        private Anuncio anuncio;
        private Pessoa pessoa;
        private Endereco endereco;

        private bool isHorarioSelecionado = false;
        private bool isAreaSelecionada = false;

        MessageDialog md = new MessageDialog("");

        private IMobileServiceTable<Anuncio> AnuncioTable = App.MobileService.GetTable<Anuncio>();
        private IMobileServiceTable<Endereco> EnderecoTable = App.MobileService.GetTable<Endereco>();

        public AnuncioPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            anuncio = new Anuncio();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }
        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width >= 960)
            {
                VisualStateManager.GoToState(this, "WideState", false);
            }
            else if (e.Size.Width < 960 && e.Size.Width > 500)
            {
                VisualStateManager.GoToState(this, "MediumState", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "NarrowState", false);
            }
        }

        private async void btn_salvar_Click(object sender, RoutedEventArgs e)
        {
            if (!tB_titulo.Text.Equals(""))
            {
                if (!tB_salario.Text.Equals(""))
                {
                    if (isHorarioSelecionado)
                    {
                        if (!tB_descricao.Text.Equals(""))
                        {
                            if (isAreaSelecionada)
                            {
                                anuncio.titulo = tB_titulo.Text;
                                anuncio.valorOuSalario = tB_salario.Text;
                                anuncio.beneficios = tB_beneficios.Text;
                                anuncio.exigencias = tB_exigencias.Text;
                                anuncio.descricao = tB_descricao.Text;
                                anuncio.isAberto = true;

                                anuncio.PessoaId = pessoa.Id;

                                anuncio.latitude = endereco.latitude;
                                anuncio.longitude = endereco.longitude;

                                if (pessoa.isPessoaFisica == true)
                                {
                                    anuncio.isFormal = false;
                                }
                                else
                                {
                                    anuncio.isFormal = true;
                                }

                                carregar.Visibility = Visibility.Visible;

                                await AnuncioTable.InsertAsync(anuncio);

                                carregar.Visibility = Visibility.Collapsed;

                                Frame.Navigate(typeof(MapaPage), pessoa);
                            }
                            else
                            {
                                md.Title = "Erro na validação!";
                                md.Content = "A área do trabalho deve ser selecionada.";
                                await md.ShowAsync();
                                Cb_area.PlaceholderText = "*Area";
                            }
                        }
                        else
                        {
                            md.Title = "Erro na validação!";
                            md.Content = "O campo Descrição é obrigatório.";
                            await md.ShowAsync();
                            tB_descricao.PlaceholderText = "*Descrição";
                        }
                    }
                    else
                    {
                        md.Title = "Erro na validação!";
                        md.Content = "O Horário deve ser selecionada.";
                        await md.ShowAsync();
                        cB_horario.PlaceholderText = "*Horário";
                    }
                }
                else
                {
                    if (pessoa.isPessoaFisica)
                    {
                        md.Title = "Erro na validação!";
                        md.Content = "O Valor para a vaga é obrigatório.";
                        await md.ShowAsync();
                        tB_salario.PlaceholderText = "*Valor";
                    }
                    else
                    {
                        md.Title = "Erro na validação!";
                        md.Content = "O Salário para a vaga é obrigatório.";
                        await md.ShowAsync();
                        tB_salario.PlaceholderText = "*Salário";
                    }
                }
            }
            else
            {
                md.Title = "Erro na validação!";
                md.Content = "O Título é obrigatório.";
                await md.ShowAsync();
                tB_titulo.PlaceholderText = "*Título";
            }

           
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Each time a navigation event occurs, update the Back button's visibility
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame.Navigate(typeof(MapaPage), pessoa);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            pessoa = (Pessoa)e.Parameter;
            carregar.Visibility = Visibility.Visible;
            var itens = await EnderecoTable.ToCollectionAsync<Endereco>();
            List<Endereco> enderecos = itens.ToList();
            foreach (Endereco end in enderecos)
            {
                if (pessoa.Id.Equals(end.PessoaId))
                {
                    endereco = end;
                }
            }

            carregar.Visibility = Visibility.Collapsed;

            if (pessoa.isPessoaFisica)
            {
                tB_salario.PlaceholderText = "Valor Proposto:";
            }
            else
            {
                tB_salario.PlaceholderText = "Salário para a vaga:";
            }

         
        }

        private void cB_horario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            anuncio.horario = valorSelecionado;
            isHorarioSelecionado = true;
        }

        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            anuncio.area = valorSelecionado;
            isAreaSelecionada = true;
        }
    }
}
