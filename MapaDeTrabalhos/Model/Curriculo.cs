using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class Curriculo
    {
        public List<ExperienciaProfissional> experienciasProfissionais { get; set; }

        public List<Idioma> idiomas { get; set; }

        public List<FormacaoAcademica> formacoesAcademicas { get; set; }

        public List<string> habilitacao { get; set; }

        public List<string> veiculoProprio { get; set; }

        public Boolean IsDisponivelViajar { get; set; }

        public Boolean IsDisponivelMudarResidencia { get; set; }

        public string valorMenorSalario { get; set; }

        public string valorMaiorSalario { get; set; }


    }
}
