using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapadetrabalhomobileserviceService.DataObjects
{
    public class Anuncio : Microsoft.WindowsAzure.Mobile.Service.EntityData
    {
        public string PessoaId { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa pessoa { get; set; }

        public string titulo { get; set; }

        public string descricao { get; set; }

        public string exigencias { get; set; }

        public string beneficios { get; set; }

        public string horario { get; set; }

        public string valorOuSalario { get; set; }

        public Boolean isFormal { get; set; }

        public Boolean isAberto { get; set; }
    }
}
