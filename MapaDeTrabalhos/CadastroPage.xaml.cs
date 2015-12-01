﻿using MapaDeTrabalhos.Model;
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
using Windows.UI.Core;
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

        private IMobileServiceTable<Pessoa> PessoaTable = App.MobileService.GetTable<Pessoa>();
        private IMobileServiceTable<Usuario> UsuarioTable = App.MobileService.GetTable<Usuario>();
        private IMobileServiceTable<Endereco> EnderecoTable = App.MobileService.GetTable<Endereco>();

        public CadastroPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;

            pessoa = new Pessoa();
            endereco = new Endereco();
            usuario = new Usuario();
            this.pessoa.sexo = "Masculino";
            this.pessoa.isPessoaFisica = true;
        }


        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width >= 720)
            {
                VisualStateManager.GoToState(this, "WideState", false);
            }
            else if (e.Size.Width < 720)
            {
                VisualStateManager.GoToState(this, "PortraitState", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "DefaultState", false);
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
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

            if(Rb_fisica.IsChecked == true)
            {
                pessoa.isPessoaFisica = true;
            }
            else
            {
                pessoa.isPessoaFisica = false;
            }

            usuario.Senha = Pb_senha.Password;

            usuario.Login = Tb_usuario.Text;

            endereco.Rua = Tb_rua.Text;

            endereco.numero = Tb_numero.Text;

            endereco.bairro = Tb_bairro.Text;
            
            endereco.cidade = Tb_cidade.Text;


            //Pegando as posições geograficas 

            string addressToGeocode = endereco.Rua + ", " + endereco.numero + ", " + endereco.bairro + ", " + endereco.cidade +" - " + endereco.estado;

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

            // If the query returns results, display the coordinates  of the first result.
            if (result.Status == MapLocationFinderStatus.Success)
            {
               endereco.latitude = result.Locations[0].Point.Position.Latitude;
               endereco.longitude = result.Locations[0].Point.Position.Longitude;
            }

            ////Salvando Pessoa
            await PessoaTable.InsertAsync(pessoa);

                  endereco.PessoaId = pessoa.Id;
            await EnderecoTable.InsertAsync(endereco);

                   usuario.PessoaId = pessoa.Id;
            await UsuarioTable.InsertAsync(usuario);
            //Fazer as paradas para salvar endereço e usuário


            //Direcionamento de Página
            if (Rb_fisica.IsChecked == true)
            {
                var resulta = await CadastroDialog.ShowAsync();
                string resultado = "" + resulta;
                if (resultado.Equals("Primary"))
                {
                    Frame.Navigate(typeof(CurriculoPage), pessoa);
                }
                else
                {
                    Frame.Navigate(typeof(MapaPage), pessoa);
                }

            }
            else
            {
                Frame.Navigate(typeof(MapaPage), pessoa);
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
            }



        }

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
