using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class ExperienciaProfissional
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "CurriculoId")]
        public string CurriculoId { get; set; }

        [JsonProperty(PropertyName = "nomeEmpresa")]
        public string nomeEmpresa { get; set; }

        [JsonProperty(PropertyName = "nomeFuncao")]
        public string nomeFuncao { get; set; }

        [JsonProperty(PropertyName = "nivelFuncao")]
        public string nivelFuncao { get; set; }

        [JsonProperty(PropertyName = "descAtividade")]
        public string descAtividade { get; set; }

        [JsonProperty(PropertyName = "area")]
        public string area { get; set; }

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

        [JsonProperty(PropertyName = "isAtual")]
        public Boolean isAtual { get; set; }
    }
}
