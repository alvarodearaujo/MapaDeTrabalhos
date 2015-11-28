using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class Idioma
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "CurriculoId")]
        public string CurriculoId { get; set; }

        [JsonProperty(PropertyName = "nomeIdioma")]
        public string nomeIdioma { get; set; }

        [JsonProperty(PropertyName = "nivelIdioma")]
        public string nivelIdioma { get; set; }
    }
}
