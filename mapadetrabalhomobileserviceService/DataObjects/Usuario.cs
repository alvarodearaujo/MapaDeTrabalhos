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
    public class Usuario : EntityData
    {
        public string PessoaId { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa pessoa { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
