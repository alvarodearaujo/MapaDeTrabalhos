using MapaDeTrabalhos.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MapaDeTrabalhos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IMobileServiceTable<Usuario> UsuarioTable = App.MobileService.GetTable<Usuario>();
        private IMobileServiceTable<Pessoa> PessoaTable = App.MobileService.GetTable<Pessoa>();
        private IMobileServiceTable<Endereco> EnderecoTable = App.MobileService.GetTable<Endereco>();
        Pessoa pessoa;
        MessageDialog dialog = new MessageDialog("");

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            pessoa = new Pessoa();
        }
        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width >= 1200)
            {
                VisualStateManager.GoToState(this, "WideState", false);
            }
            else if (e.Size.Width < 1200 && e.Size.Width > 600)
            {
                VisualStateManager.GoToState(this, "MediumState", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "PortraitState", false);
            }
        }

        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CadastroPage));
        }

        private async void Logar_Click(object sender, RoutedEventArgs e)
        {
            string login = Tb_login.Text;
            string senha = Tb_senha.Password;
            carregar.Visibility = Visibility.Visible;
            var itens = await UsuarioTable.ToCollectionAsync<Usuario>();
            List<Usuario> usuarios = itens.ToList();

            bool achouUsuario = false;
            bool achouSenha = false;
            foreach (Usuario user in usuarios)
            {
                if (user.Login.Equals(login))
                {
                    achouUsuario = true;
                    if (user.Senha.Equals(senha))
                    {
                        achouSenha = true;
                        var itens2 = await PessoaTable.ToCollectionAsync<Pessoa>();
                        List<Pessoa> pessoas = itens2.ToList();

                        foreach (Pessoa person in pessoas)
                        {
                            if (user.PessoaId.Equals(person.Id))
                            {
                                Frame.Navigate(typeof(MapaPage), person);
                                break;
                            }

                        }
                    }
                }
            }

            carregar.Visibility = Visibility.Collapsed;
            if (achouUsuario == true && achouSenha == false)
            {
                dialog.Content = "Senha Incorreta!";
                await dialog.ShowAsync();
            }
            else if (achouUsuario == false)
            {
                dialog.Content = "Usuário não encontrado!";
                await dialog.ShowAsync();
            }


        }
    }
}
