using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mapadetrabalhomobileserviceService.DataObjects
{
    public class Curriculo
    {
        public string PessoaId { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa pessoa { get; set; }

        public List<string> habilitacao { get; set; }

        public List<string> veiculoProprio { get; set; }

        public Boolean IsDisponivelViajar { get; set; }

        public Boolean IsDisponivelMudarResidencia { get; set; }

        public string valorMenorSalario { get; set; }

        public string valorMaiorSalario { get; set; }

    }
}