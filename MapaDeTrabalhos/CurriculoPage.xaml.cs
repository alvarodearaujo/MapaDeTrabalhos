using MapaDeTrabalhos.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapaDeTrabalhos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurriculoPage : Page
    {
        private byte[] CurrentImage { get; set; }

        private Pessoa pessoa;
        private IMobileServiceTable<Curriculo> CurriculoTable = App.MobileService.GetTable<Curriculo>();
        private IMobileServiceTable<ExperienciaProfissional> ExperienciaTable = App.MobileService.GetTable<ExperienciaProfissional>();
        private IMobileServiceTable<Idioma> IdiomaTable = App.MobileService.GetTable<Idioma>();
        private IMobileServiceTable<FormacaoAcademica> FormacaoTable = App.MobileService.GetTable<FormacaoAcademica>();

        List<ExperienciaProfissional> listaExperiencias;
        List<Idioma> listaIdiomas;
        List<FormacaoAcademica> listaFormacao;

        private Curriculo curriculo;
        string statusCurso = "";
        string statusExperiencia = "";
        FormacaoAcademica form = new FormacaoAcademica();
        ExperienciaProfissional expe = new ExperienciaProfissional();
        Idioma idi = new Idioma();


        MessageDialog dialog = new MessageDialog("");

        public CurriculoPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            curriculo = new Curriculo();
            curriculo.IsDisponivelMudarResidencia = true;
            curriculo.IsDisponivelViajar = true;

            listaExperiencias = new List<ExperienciaProfissional>();
            listaFormacao = new List<FormacaoAcademica>();
            listaIdiomas = new List<Idioma>();

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

        private async void Salvar_Click(object sender, RoutedEventArgs e)
        {
            //Fazer as validações 
            if(Rb_mudarNao.IsChecked == true)
            {
                curriculo.IsDisponivelMudarResidencia = false;
            }
            else
            {
                curriculo.IsDisponivelMudarResidencia = true;
            }

            if(Rb_viajarNao.IsChecked == true)
            {
                curriculo.IsDisponivelViajar = false;
            }
            else
            {
                curriculo.IsDisponivelViajar = false;
            }


            if (HabilitacaoA.IsChecked == true)
            {
                curriculo.habilitacao.Add("A");
            }
            else if (HabilitacaoB.IsChecked == true)
            {
                curriculo.habilitacao.Add("B");
            }
            else if (HabilitacaoC.IsChecked == true)
            {
                curriculo.habilitacao.Add("C");
            }
            else if (HabilitacaoD.IsChecked == true)
            {
                curriculo.habilitacao.Add("D");

            }
            else if (HabilitacaoE.IsChecked == true)
            {
                curriculo.habilitacao.Add("E");
            }

            if (Caminhao.IsChecked == true)
            {
                curriculo.veiculoProprio.Add("Caminhão");
            }
            else if (Carro.IsChecked == true)
            {
                curriculo.veiculoProprio.Add("Carro");
            }
            else if (Moto.IsChecked == true)
            {
                curriculo.veiculoProprio.Add("Moto");
            }
            else if (Outros.IsChecked == true)
            {
                curriculo.veiculoProprio.Add("Outro veículo");
            }
            curriculo.Foto = CurrentImage;

            carregar.Visibility = Visibility.Visible;
            await CurriculoTable.InsertAsync(curriculo);
            carregar.Visibility = Visibility.Collapsed;

            Frame.Navigate(typeof(MapaPage), pessoa);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pessoa = (Pessoa)e.Parameter;
        }

        private async void AdicionarFormacao_Click(object sender, RoutedEventArgs e)
        {
            var result = await FormacaoDialog.ShowAsync();

            string resutado = "" + result;
            if (resutado.Equals("Primary"))
            {
                // vai fazer as validações dos campos

                if (statusCurso.Equals("Cursando"))
                {
                    form.isCursando = true;

                }
                else if (statusCurso.Equals("Concluido"))
                {
                    form.isCursando = false;
                }
                else
                {
                    form.isTrancado = true;
                }
                form.anoFinal = anoFinal.Text;
                form.anoInicial = anoInicial.Text;
                form.mesFinal = mesFinal.Text;
                form.mesInicial = mesInicial.Text;
                await FormacaoTable.InsertAsync(form);
                listaFormacao.Add(form);
                Lv_formacao.ItemsSource = listaFormacao;

                carregar.Visibility = Visibility.Visible;
                form = new FormacaoAcademica();
                carregar.Visibility = Visibility.Collapsed;
            }
        }

        private async void AdicionarExperiencia_Click(object sender, RoutedEventArgs e)
        {
            var result = await ExperienciaDialog.ShowAsync();
            string resutado = "" + result;
            if (resutado.Equals("Primary"))
            {
                //Fazer as validaçoes
                expe = new ExperienciaProfissional();
                expe.nomeEmpresa = Tb_nomeEmpresa.Text;
                expe.nomeFuncao = Tb_nomeCargo.Text;
                expe.anoFinal = anoFinalExperiencia.Text;
                expe.anoInicial = anoFinalExperiencia.Text;
                expe.mesInicial = mesInicialExperiencia.Text;
                expe.mesFinal = mesFinal.Text;

                await ExperienciaTable.InsertAsync(expe);
                listaExperiencias.Add(expe);
                Lv_experiencia.ItemsSource = listaExperiencias;

                carregar.Visibility = Visibility.Visible;
                expe = new ExperienciaProfissional();
                carregar.Visibility = Visibility.Collapsed;
            }
        }

        private async void AdicionarIdioma_Click(object sender, RoutedEventArgs e)
        {
            //Validar os campos
            var result = await IdiomaDialog.ShowAsync();
            string resutado = "" + result;
            if (resutado.Equals("Primary"))
            {
                if (idi.nomeIdioma.Equals("Outro"))
                {
                    idi.nomeIdioma = Tb_nomeIdioma.Text;
                }

                await IdiomaTable.InsertAsync(idi);
                listaIdiomas.Add(idi);
                Lv_idioma.ItemsSource = listaIdiomas;
                carregar.Visibility = Visibility.Visible;
                idi = new Idioma();
                carregar.Visibility = Visibility.Collapsed;
            }
        }

        private void Rb_Concluido_Checked(object sender, RoutedEventArgs e)
        {
            if (mesFinal != null)
            {
                mesFinal.IsEnabled = true;
                anoFinal.IsEnabled = true;
                statusCurso = "Concluido";
            }

        }

        private void Rb_Cursando_Checked(object sender, RoutedEventArgs e)
        {
            if (mesFinal != null)
            {
                mesFinal.IsEnabled = false;
                anoFinal.IsEnabled = false;
                statusCurso = "Cursando";
            }
        }

        private void Rb_Trancado_Checked(object sender, RoutedEventArgs e)
        {
            if (mesFinal != null)
            {
                mesFinal.IsEnabled = false;
                anoFinal.IsEnabled = false;
                statusCurso = "Trancado";
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (mesFinalExperiencia != null)
            {
                if (Cb_atual.IsChecked == true)
                {
                    mesFinalExperiencia.IsEnabled = false;
                    anoFinalExperiencia.IsEnabled = false;
                    statusExperiencia = "Nao";
                }
                else
                {
                    statusExperiencia = "Sim";
                }

            }
        }

        private void CheckBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (mesFinalExperiencia != null)
            {
                if (mesFinalExperiencia.IsEnabled == true)
                {
                    mesFinalExperiencia.IsEnabled = false;
                    anoFinalExperiencia.IsEnabled = false;
                }
                else
                {
                    mesFinalExperiencia.IsEnabled = true;
                    anoFinalExperiencia.IsEnabled = true;
                }
            }
        }

        private async void image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                FileOpenPicker fop = new FileOpenPicker();
                fop.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

                fop.FileTypeFilter.Add(".jpg");
                fop.FileTypeFilter.Add(".jpeg");
                fop.FileTypeFilter.Add(".png");
                fop.FileTypeFilter.Add(".gif");

                var file = await fop.PickSingleFileAsync();
                if (file != null)
                {
                    var stream = await file.OpenAsync(FileAccessMode.Read);

                    BitmapImage bp = new BitmapImage();
                    bp.SetSource(stream);
                    imgPhoto.ImageSource = bp;
                    CurrentImage = await Functions.ConvertStorageFileToArrayBytes(file);
                    //PhotoName = file.Name;
                }
            }
            catch (Exception ex)
            {
                dialog.Title = "Erro";
                dialog.Content = ex.Message.ToString();
                await dialog.ShowAsync();
            }
        }

        private void Cb_PaisExperiencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();

            if (Sp_estadoExperiencia != null)
            {
                if (valorSelecionado.Equals("Brasil"))
                {
                    Sp_estadoExperiencia.Visibility = Visibility.Visible;
                }
                else
                {
                    Sp_estadoExperiencia.Visibility = Visibility.Collapsed;
                }
            }
            expe.pais = valorSelecionado;

        }

        private void Cb_Pais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();

            if (Sp_estado != null)
            {
                if (valorSelecionado.Equals("Brasil"))
                {
                    Sp_estado.Visibility = Visibility.Visible;
                }
                else
                {
                    Sp_estado.Visibility = Visibility.Collapsed;
                }
            }
            form.pais = valorSelecionado;
        }

        private void Cb_NivelFormacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            form.nivelCurso = valorSelecionado;
            if (valorSelecionado.Equals("Ensino Fundamental (1° Grau)"))
            {
                Cb_NomeDoCurso.ItemsSource = "Ensino Fundamental(1° Grau)";
            }
            else if (valorSelecionado.Equals("Ensino Médio (2° Grau)"))
            {
                Cb_NomeDoCurso.ItemsSource = "Ensino Médio (2° Grau)";
            }
            else if (valorSelecionado.Equals("Curso Extra-Curricular / Profissionalizante"))
            {
                Cb_NomeDoCurso.ItemsSource = Cursos.listarExtraCurriculares();
            }
            else if (valorSelecionado.Equals("Curso Técnico"))
            {
                Cb_NomeDoCurso.ItemsSource = Cursos.listaTecnicos();
            }
            else if (valorSelecionado.Equals("Ensino Superior"))
            {
                Cb_NomeDoCurso.ItemsSource = Cursos.listarSuperior();
            }
            else if (valorSelecionado.Equals("Pós Graduação / Especialização / MBA"))
            {
                Cb_NomeDoCurso.ItemsSource = Cursos.listarPos();
            }
            else if (valorSelecionado.Equals("Pós Graduação / Mestrado"))
            {
                Cb_NomeDoCurso.ItemsSource = Cursos.listarMestrado();
            }
            else if (valorSelecionado.Equals("Pós Graduação / Doutorado"))
            {
                Cb_NomeDoCurso.ItemsSource = Cursos.listarDoutorado();
            }

        }

        private void Cb_NomeDoCurso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string valorSelecionado = Cb_NomeDoCurso.SelectedValue.ToString();
            form.nomeCurso = valorSelecionado;
        }

        private void Cb_Estados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            form.estado = valorSelecionado;
        }

        private void Cb_NivelCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            expe.nivelFuncao = valorSelecionado;
        }

        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            expe.area = valorSelecionado;
        }

        private void Cb_EstadosExperiencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            expe.estado = valorSelecionado;
        }

        private void Cb_Idiomas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();

            if (valorSelecionado.Equals("Outro"))
            {
                Sp_outroIdioma.Visibility = Visibility.Visible;
                idi.nomeIdioma = "Outro";
            }
            else
            {
                Sp_outroIdioma.Visibility = Visibility.Collapsed;
                idi.nomeIdioma = valorSelecionado;
            }

        }

        private void Cb_NivelIdiomas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();
            idi.nivelIdioma = valorSelecionado;
        }

        private void Rb_viajarSim_Click(object sender, RoutedEventArgs e)
        {
            curriculo.IsDisponivelViajar = true;
        }

        private void Rb_viajarNao_Click(object sender, RoutedEventArgs e)
        {
            curriculo.IsDisponivelViajar = false;
        }

        private void Rb_mudarSim_Click(object sender, RoutedEventArgs e)
        {
            curriculo.IsDisponivelMudarResidencia = true;
        }

        private void Rb_mudarNao_Click(object sender, RoutedEventArgs e)
        {
            curriculo.IsDisponivelMudarResidencia = false;
        }
    }
}
