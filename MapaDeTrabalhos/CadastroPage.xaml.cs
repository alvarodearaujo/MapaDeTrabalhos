using MapaDeTrabalhos.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
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
    public sealed partial class CadastroPage : Page
    {
        private Pessoa pessoa;

        private Usuario usuario;

        private Endereco endereco;

        private IMobileServiceTable<Pessoa> ContatoTable = App.MobileService.GetTable<Pessoa>();

        public CadastroPage()
        {
            this.InitializeComponent();
            pessoa = new Pessoa();
            endereco = new Endereco();
            usuario = new Usuario();
        }

        private async void Salvar_Click(object sender, RoutedEventArgs e)
        {
            ////Fazer o processo pra validar os campos de cadastro e salvar no banco.

            pessoa.nomeOuRazaoSocial = Tb_nome.Text;

            pessoa.data = Tb_dataNascimento.Text;

            pessoa.cpfOuCnpj = Tb_cpfOrCnpj.Text;

            pessoa.email = Tb_email.Text;

            pessoa.celular = Tb_celular.Text;

            pessoa.telefone = Tb_telefone.Text;

            pessoa.site = Tb_site.Text;

            usuario.Senha = Pb_senha.Password;

            usuario.Login = Tb_usuario.Text;

            endereco.Rua = Tb_rua.Text;

            endereco.numero = Tb_numero.Text;

            endereco.bairro = Tb_bairro.Text;
            
            endereco.cidade = Tb_cidade.Text;

            //Pegando as posições geograficas 

            string addressToGeocode = endereco.Rua + ", " + endereco.numero + ", " + endereco.bairro + ", " + endereco.cidade +" - " + endereco.estado;

            //// Nearby location to use as a query hint.
            //BasicGeoposition queryHint = new BasicGeoposition();
            //queryHint.Latitude = -8.05665;
            //queryHint.Longitude = -34.898441;
            //Geopoint hintPoint = new Geopoint(queryHint);

            //// Geocode the specified address, using the specified reference point
            //// as a query hint. Return no more than 3 results.
            //MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);

            ////Setting position default to Derby - Recife
            //BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = -8.05665, Longitude = -34.898441 };
            //Geopoint cityCenter = new Geopoint(cityPosition);
            //// If the query returns results, display the coordinates
            // of the first result.
            //if (result.Status == MapLocationFinderStatus.Success)
            //{
            //    pessoa.endereco.latitude = result.Locations[0].Point.Position.Latitude;
            //    pessoa.endereco.longitude = result.Locations[0].Point.Position.Longitude;
            //}

            ////Salvando Pessoa
            //await App.MobileService.GetTable<Pessoa>().UpdateAsync(pessoa);

            //Fazer as paradas para salvar endereço e usuário


            //Direcionamento de Página
            var resulta = await CadastroDialog.ShowAsync();
            string resutado = "" + resulta;
            if (resutado.Equals("Primary"))
            {
                Frame.Navigate(typeof(CurriculoPage));
            }
            else
            {
                Frame.Navigate(typeof(MapaPage));
            }


        }


        private void Rb_fisica_Checked(object sender, RoutedEventArgs e)
        {
            //fazer a mascara de CPF
            if (Sp_sexo != null)
            {
                Sp_sexo.Visibility = Visibility.Visible;
                Tb_dataNascimento.PlaceholderText = "Data de nascimento:";
                Tb_nome.PlaceholderText = "Nome:";
                Tb_cpfOrCnpj.PlaceholderText = "CPF:";
                Tb_site.Visibility = Visibility.Collapsed;
                this.pessoa.isPessoaFisica = true;
            }


        }

        private void Rb_juridica_Checked(object sender, RoutedEventArgs e)
        {

            if (Sp_sexo != null)
            {
                Sp_sexo.Visibility = Visibility.Collapsed;
                Tb_dataNascimento.PlaceholderText = "Data de Fundação:";
                Tb_nome.PlaceholderText = "Razão social:";
                Tb_cpfOrCnpj.PlaceholderText = "CNPJ:";
                Tb_site.Visibility = Visibility.Visible;
                this.pessoa.isPessoaFisica = false;
                //Fazer a Ideia do CNPJ
            }



        }
        //Setando o Estado
        private void Cb_estados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            endereco.estado = valorSelecionado;

        }


        private void Rb_masc_Checked(object sender, RoutedEventArgs e)
        {
            if (pessoa != null)
            {
                this.pessoa.sexo = "Masculino";
            }
        }

        private void Rb_femi_Checked(object sender, RoutedEventArgs e)
        {
            if (pessoa != null)
            {
                this.pessoa.sexo = "Feminino";
            }

        }
    }
}
