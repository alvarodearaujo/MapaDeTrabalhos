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

        private IMobileServiceTable<Anuncio> AnuncioTable = App.MobileService.GetTable<Anuncio>();

        public AnuncioPage()
        {
            this.InitializeComponent();
            anuncio = new Anuncio();
        }

        private async void btn_salvar_Click(object sender, RoutedEventArgs e)
        {
            anuncio.titulo = tB_titulo.Text;
            anuncio.valorOuSalario = tB_salario.Text;
            anuncio.beneficios = tB_beneficios.Text;
            anuncio.exigencias = tB_exigencias.Text;
            anuncio.descricao = tB_descricao.Text;

            if (rB_formal.IsChecked == true)
            {
                anuncio.isFormal = true;
            }
            if (rB_informal.IsChecked == true)
            {
                anuncio.isAberto = true;
            }
            await AnuncioTable.InsertAsync(anuncio);
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
