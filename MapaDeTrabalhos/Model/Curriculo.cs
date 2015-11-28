using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class Curriculo
    {

        public string Id { get; set; }

        [JsonProperty(PropertyName = "PessoaId")]
        public string PessoaId { get; set; }

        [JsonProperty(PropertyName = "habilitacao")]
        public List<string> habilitacao { get; set; }

        [JsonProperty(PropertyName = "veiculoProprio")]
        public List<string> veiculoProprio { get; set; }

        [JsonProperty(PropertyName = "IsDisponivelViajar")]
        public Boolean IsDisponivelViajar { get; set; }

        [JsonProperty(PropertyName = "IsDisponivelMudarResidencia")]
        public Boolean IsDisponivelMudarResidencia { get; set; }

        [JsonProperty(PropertyName = "valorMenorSalario")]
        public string valorMenorSalario { get; set; }

        [JsonProperty(PropertyName = "valorMaiorSalario")]
        public string valorMaiorSalario { get; set; }


    }
}
