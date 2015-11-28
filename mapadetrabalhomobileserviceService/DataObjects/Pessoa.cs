using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapadetrabalhomobileserviceService.DataObjects
{
    public class Pessoa : Microsoft.WindowsAzure.Mobile.Service.EntityData
    {
     
        public string nomeOuRazaoSocial { get; set; }

        public string cpfOuCnpj { get; set; }

        public string telefone { get; set; }

        public string email { get; set; }

        public string celular { get; set; }

        public Boolean isPessoaFisica { get; set; }

      
        public Endereco endereco { get; set; }

        public Usuario usuario { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }



    }
}
