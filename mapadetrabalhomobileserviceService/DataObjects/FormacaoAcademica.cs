using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mapadetrabalhomobileserviceService.DataObjects
{
    public class FormacaoAcademica : EntityData
    {
        [ForeignKey("CurriculoId")]
        public virtual Curriculo curriculo { get; set; }

        public string nivelCurso { get; set; }

        public string nomeCurso { get; set; }

        public string mesInicial { get; set; }

        public string anoInicial { get; set; }

        public string mesFinal { get; set; }

        public string anoFinal { get; set; }

        public string estado { get; set; }

        public string pais { get; set; }

        public Boolean isCursando { get; set; }

        public Boolean isTrancado { get; set; }
    }
}