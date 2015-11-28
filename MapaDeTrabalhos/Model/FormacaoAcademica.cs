using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class FormacaoAcademica
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "CurriculoId")]
        public string CurriculoId { get; set; }

        [JsonProperty(PropertyName = "nivelCurso")]
        public string nivelCurso { get; set; }

        [JsonProperty(PropertyName = "nomeCurso")]
        public string nomeCurso { get; set; }

        [JsonProperty(PropertyName = "mesInicial")]
        public string mesInicial { get; set; }

        [JsonProperty(PropertyName = "anoInicial")]
        public string anoInicial { get; set; }

        [JsonProperty(PropertyName = "mesFinal")]
        public string mesFinal { get; set; }

        [JsonProperty(PropertyName = "anoFinal")]
        public string anoFinal { get; set; }

        [JsonProperty(PropertyName = "estado")]
        public string estado { get; set; }

        [JsonProperty(PropertyName = "pais")]
        public string pais { get; set; }

        [JsonProperty(PropertyName = "isCursando")]
        public Boolean isCursando { get; set; }

        [JsonProperty(PropertyName = "isTrancado")]
        public Boolean isTrancado { get; set; }
    }
}
