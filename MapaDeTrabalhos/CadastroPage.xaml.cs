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
    public sealed partial class CadastroPage : Page
    {
        public CadastroPage()
        {
            this.InitializeComponent();
            
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            //Fazer o processo pra validar os campos de cadastro e salvar no banco.
        }
      

        private void Rb_fisica_Checked(object sender, RoutedEventArgs e)
        {
            //fazer a mascara de CPF
            
            Sp_sexo.Visibility = Visibility.Visible;

        }

        private void Rb_juridica_Checked(object sender, RoutedEventArgs e)
        {
            //fazer a mascara de CNPJ
            Sp_sexo.Visibility = Visibility.Collapsed;
        }
    }
}
