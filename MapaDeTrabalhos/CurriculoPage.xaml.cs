using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        public byte[] CurrentImage { get; set; }

        MessageDialog dialog = new MessageDialog("");

        public CurriculoPage()
        {
            this.InitializeComponent();
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {

        }


        private async void AdicionarFormacao_Click(object sender, RoutedEventArgs e)
        {
            var result = await FormacaoDialog.ShowAsync();

        }

        private async void AdicionarExperiencia_Click(object sender, RoutedEventArgs e)
        {
            var result = await ExperienciaDialog.ShowAsync();
        }

        private async void AdicionarIdioma_Click(object sender, RoutedEventArgs e)
        {
            var result = await IdiomaDialog.ShowAsync();
        }

        private void Rb_Concluido_Checked(object sender, RoutedEventArgs e)
        {
            if (mesFinal != null)
            {
                mesFinal.IsEnabled = true;
                anoFinal.IsEnabled = true;
            }

        }

        private void Rb_Cursando_Checked(object sender, RoutedEventArgs e)
        {
            if (mesFinal != null)
            {
                mesFinal.IsEnabled = false;
                anoFinal.IsEnabled = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (mesFinalExperiencia != null)
            {
                mesFinalExperiencia.IsEnabled = false;
                anoFinalExperiencia.IsEnabled = false;
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


        private void Cb_Idiomas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            string valorSelecionado = item.Content.ToString();

            if (valorSelecionado.Equals("Outro"))
            {
                Sp_outroIdioma.Visibility = Visibility.Visible;
            }
            else
            {
                Sp_outroIdioma.Visibility = Visibility.Collapsed;
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
    }
}
