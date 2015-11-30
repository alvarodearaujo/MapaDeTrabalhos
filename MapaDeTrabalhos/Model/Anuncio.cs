using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    public class Anuncio
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "PessoaId")]
        public string PessoaId { get; set; }

        [JsonProperty(PropertyName = "titulo")]
        public string titulo { get; set; }

        [JsonProperty(PropertyName = "descricao")]
        public string descricao { get; set; }

        [JsonProperty(PropertyName = "exigencias")]
        public string exigencias { get; set; }

        [JsonProperty(PropertyName = "beneficios")]
        public string beneficios { get; set; }

        [JsonProperty(PropertyName = "horario")]
        public string horario { get; set; }

        [JsonProperty(PropertyName = "valorOuSalario")]
        public string valorOuSalario { get; set; }

        [JsonProperty(PropertyName = "isFormal")]
        public Boolean isFormal { get; set; }

        [JsonProperty(PropertyName = "isAberto")]
        public Boolean isAberto { get; set; }

        [JsonProperty(PropertyName = "area")]
        public string area { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double longitude { get; set; }


    }
}
