using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class FormacaoAcademica
    {
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
