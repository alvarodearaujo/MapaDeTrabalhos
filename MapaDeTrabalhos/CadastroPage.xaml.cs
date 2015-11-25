using MapaDeTrabalhos.Model;
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
        private Pessoa pessoa;
        public CadastroPage()
        {
            this.InitializeComponent();
            pessoa = new Pessoa();
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            //Fazer o processo pra validar os campos de cadastro e salvar no banco.
        }
      

        private void Rb_fisica_Checked(object sender, RoutedEventArgs e)
        {
            //fazer a mascara de CPF
            if(Sp_sexo!= null)
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
                //Fazer a Ideia do CNPJ
            }



        }
    }
}
