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
    public sealed partial class CadastroPage : Page
    {
        private Pessoa pessoa;

        private Usuario usuario;

        private Endereco endereco;

        private bool isEstadoSelecionado = false;

        private bool isCpfValido = false;

        private bool isEmailValido = false;

        MessageDialog md = new MessageDialog("");

        private IMobileServiceTable<Pessoa> PessoaTable = App.MobileService.GetTable<Pessoa>();
        private IMobileServiceTable<Usuario> UsuarioTable = App.MobileService.GetTable<Usuario>();
        private IMobileServiceTable<Endereco> EnderecoTable = App.MobileService.GetTable<Endereco>();

        public CadastroPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;

            
            endereco = new Endereco();
            usuario = new Usuario();
            pessoa = new Pessoa();
            this.pessoa.sexo = "Masculino";
            this.pessoa.isPessoaFisica = true;

            // visibility of the Back button
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

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if(pessoa != null && pessoa.Id != null && !pessoa.Id.Equals(""))
            {
                Frame.Navigate(typeof(MapaPage), pessoa);
            }
            else
            {
                Frame.Navigate(typeof(MainPage));
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pessoa = (Pessoa)e.Parameter;
        }

        private async void Salvar_Click(object sender, RoutedEventArgs e)
        {
            if (!Tb_nome.Text.Equals(""))
            {
                if (isCpfValido)
                {
                    if (isEmailValido)
                    {
                        if (!Tb_rua.Text.Equals(""))
                        {
                            if (!Tb_bairro.Text.Equals(""))
                            {
                                if (!Tb_cidade.Text.Equals(""))
                                {
                                    if (isEstadoSelecionado)
                                    {
                                        if (!Tb_usuario.Text.Equals(""))
                                        {
                                            if (!Pb_senha.Password.Equals(""))
                                            {
                                                carregar.Visibility = Visibility.Visible;
                                                //Pegando as posições geograficas 
                                                string addressToGeocode = Tb_rua.Text + ", " + Tb_numero.Text + ", " + Tb_bairro.Text + ", " + Tb_cidade.Text + " - " + endereco.estado;

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
                                                    
                                                    if(pessoa == null)
                                                    {
                                                        pessoa = new Pessoa();
                                                        if (Rb_fisica.IsChecked == true)
                                                        {
                                                            pessoa.isPessoaFisica = true;

                                                            if(Rb_femi.IsChecked == true)
                                                            {
                                                                pessoa.sexo = "Feminino";
                                                            }
                                                            else
                                                            {
                                                                pessoa.sexo = "Masculino";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            pessoa.isPessoaFisica = false;
                                                            pessoa.site = Tb_site.Text;
                                                        }
                                                    }

                                                    pessoa.nomeOuRazaoSocial = Tb_nome.Text;

                                                    pessoa.data = Tb_dataNascimento.Text;

                                                    pessoa.cpfOuCnpj = Tb_cpfOrCnpj.Text;

                                                    pessoa.email = Tb_email.Text;

                                                    pessoa.celular = Tb_celular.Text;

                                                    pessoa.telefone = Tb_telefone.Text;

                                                    usuario.Senha = Pb_senha.Password;

                                                    usuario.Login = Tb_usuario.Text;

                                                    endereco.Rua = Tb_rua.Text;

                                                    endereco.numero = Tb_numero.Text;

                                                    endereco.bairro = Tb_bairro.Text;

                                                    endereco.cidade = Tb_cidade.Text;


                                                    ////Salvando Pessoa
                                                    await PessoaTable.InsertAsync(pessoa);

                                                    endereco.PessoaId = pessoa.Id;
                                                    await EnderecoTable.InsertAsync(endereco);

                                                    usuario.PessoaId = pessoa.Id;
                                                    await UsuarioTable.InsertAsync(usuario);

                                                    carregar.Visibility = Visibility.Collapsed;

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
                                                else
                                                {
                                                    md.Title = "Erro no Endereço!";
                                                    md.Content = "Não foi possível localizar o endereço no mapa, verifique as informações passadas.";
                                                    await md.ShowAsync();
                                                }

                                            }
                                            else
                                            {
                                                md.Title = "Campo obrigatório não preechido!";
                                                md.Content = "Preencha o campo Senha.";
                                                await md.ShowAsync();
                                                Pb_senha.PlaceholderText = "*Senha";
                                            }
                                        }
                                        else
                                        {
                                            md.Title = "Campo obrigatório não preechido!";
                                            md.Content = "Preencha o campo Usuário.";
                                            await md.ShowAsync();
                                            Tb_usuario.PlaceholderText = "*Usuário";
                                        }
                                    }
                                    else
                                    {
                                        md.Title = "Campo obrigatório não preechido!";
                                        md.Content = "Selecione o estado corretamente.";
                                        await md.ShowAsync();
                                        Cb_estados.PlaceholderText = "*Estado";
                                    }
                                }
                                else
                                {
                                    md.Title = "Campo obrigatório não preechido!";
                                    md.Content = "Preencha o campo cidade.";
                                    await md.ShowAsync();
                                    Tb_cidade.PlaceholderText = "*Cidade";
                                }
                            }
                            else
                            {
                                md.Title = "Campo obrigatório não preechido!";
                                md.Content = "Preencha o campo bairro.";
                                await md.ShowAsync();
                                Tb_bairro.PlaceholderText = "*Bairro";
                            }
                        }
                        else
                        {
                            md.Title = "Campo obrigatório não preechido!";
                            md.Content = "Preencha o campo rua.";
                            await md.ShowAsync();
                            Tb_rua.PlaceholderText = "*Rua";
                        }
                    }
                    else
                    {
                        md.Title = "Campo obrigatório não preechido!";
                        md.Content = "Preencha o campo E-mail.";
                        await md.ShowAsync();
                        Tb_email.PlaceholderText = "*E-mail";
                    }
                }
                else
                {
                    if (Rb_juridica.IsChecked == false)
                    {
                        md.Title = "Campo obrigatório não preechido!";
                        md.Content = "Preencha o campo CPF.";
                        await md.ShowAsync();
                        Tb_cpfOrCnpj.PlaceholderText = "*CPF";
                    }
                    else
                    {
                        md.Title = "Campo obrigatório não preechido!";
                        md.Content = "Preencha o campo CNPJ.";
                        await md.ShowAsync();
                        Tb_cpfOrCnpj.PlaceholderText = "*CNPJ";
                    }
                }
            }
            else
            {
                md.Title = "Campo obrigatório não preechido!";
                md.Content = "Preencha o campo nome.";
                await md.ShowAsync();
                Tb_nome.PlaceholderText = "*Nome";
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
            isEstadoSelecionado = true;

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

        public static bool ValidarEmail(string Email)
        {
            bool Validar = false;
            int Analizar = Email.IndexOf("@");
            if (Analizar > 0)
            {
                if (Email.IndexOf("@", Analizar + 1) > 0)
                {
                    return Validar;
                }
                int i = Email.IndexOf(".", Analizar);
                if (i - 1 > Analizar)
                {
                    if (i + 1 < Email.Length)
                    {
                        string r = Email.Substring(i + 1, 1);
                        if (r != ".")
                        {
                            Validar = true;
                        }
                    }
                }
            }
            return Validar;
        }

        public static bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static string MascaraCpf(string mCpf)
        {
            string result = "";

            if (mCpf.Length == 11)
            {
                result = mCpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            }
            if (mCpf.Length != 11)
            {
                result = mCpf;
            }


            return result;

        }

        public static string MascaraCnpj(string mCnpj)
        {
            string result = "";


            if (mCnpj.Length == 14)
            {
                result = mCnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            }

            if ((mCnpj.Length != 14))
            {
                result = mCnpj;
            }

            return result;

        }

        private async void Tb_cpfOrCnpj_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Tb_cpfOrCnpj.Text.Equals(""))
            {
                if (Rb_fisica.IsChecked == true)
                {
                    bool result = ValidarCpf(Tb_cpfOrCnpj.Text);
                    if (result == false)
                    {
                        md.Title = "Erro na validação!";
                        md.Content = "CPF inválido.";
                        await md.ShowAsync();
                        Tb_cpfOrCnpj.Text = "";
                        isCpfValido = false;
                    }
                    else
                    {
                        isCpfValido = true;
                    }

                }
                else
                {
                    bool result = ValidarCnpj(Tb_cpfOrCnpj.Text);
                    if (result == false)
                    {
                        md.Title = "Erro na validação!";
                        md.Content = "CNPJ inválido.";
                        await md.ShowAsync();
                        Tb_cpfOrCnpj.Text = "";
                        isCpfValido = false;
                    }
                    else
                    {
                        isCpfValido = true;
                    }
                }
            }
        }

        private async void Tb_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Tb_email.Equals(""))
            {
                bool result = ValidarEmail(Tb_email.Text);
                if (result)
                {
                    isEmailValido = true;
                }
                else
                {
                    md.Title = "Erro na validação!";
                    md.Content = "Email inválido.";
                    await md.ShowAsync();
                    Tb_email.Text = "";
                    isEmailValido = false;
                }
            }
        }
    }
}
