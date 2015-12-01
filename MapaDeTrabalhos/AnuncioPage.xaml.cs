using MapaDeTrabalhos.Model;
using Microsoft.WindowsAzure.MobileServices;
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

        private IMobileServiceTable<Anuncio> AnuncioTable = App.MobileService.GetTable<Anuncio>();
        private IMobileServiceTable<Endereco> EnderecoTable = App.MobileService.GetTable<Endereco>();

        public AnuncioPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            anuncio = new Anuncio();
        }
        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width >= 720)
            {
                VisualStateManager.GoToState(this, "WideState", false);
            }
            else if (e.Size.Height > e.Size.Width)
            {
                VisualStateManager.GoToState(this, "PortraitState", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "DefaultState", false);
            }
        }

        private async void btn_salvar_Click(object sender, RoutedEventArgs e)
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

            await AnuncioTable.InsertAsync(anuncio);

            Frame.Navigate(typeof(MapaPage), pessoa);
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

        }

        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            anuncio.area = valorSelecionado;
        }
    }
}
