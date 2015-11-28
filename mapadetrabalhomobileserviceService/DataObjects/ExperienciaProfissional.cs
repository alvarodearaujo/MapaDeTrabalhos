using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mapadetrabalhomobileserviceService.DataObjects
{
    public class ExperienciaProfissional
    {
        public string CurriculoId { get; set; }

        [ForeignKey("CurriculoId")]
        public virtual Curriculo curriculo { get; set; }

        public string nomeEmpresa { get; set; }

        public string nomeFuncao { get; set; }

        public string nivelFuncao { get; set; }

        public string descAtividade { get; set; }

        public string area { get; set; }

        public string mesInicial { get; set; }

        public string anoInicial { get; set; }

        public string mesFinal { get; set; }

        public string anoFinal { get; set; }

        public string estado { get; set; }

        public string pais { get; set; }

        public Boolean isAtual { get; set; }
    }
}