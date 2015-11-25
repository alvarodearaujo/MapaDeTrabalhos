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
    public class Endereco : Microsoft.WindowsAzure.Mobile.Service.EntityData
    {
        public string PessoaId { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa pessoa { get; set; }

        public string Rua { get; set; }

        public string numero { get; set; }

        public string bairro { get; set; }

        public string cidade { get; set; }

        public string estado { get; set; }
    }
}
