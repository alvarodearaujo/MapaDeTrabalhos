using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class Endereco
    {
        
        public string Id { get; set; }

        [JsonProperty(PropertyName = "PessoaId")]
        public string PessoaId { get; set; }

        [JsonProperty(PropertyName = "Rua")]
        public string Rua { get; set; }

        [JsonProperty(PropertyName = "numero")]
        public string numero { get; set; }

        [JsonProperty(PropertyName = "bairro")]
        public string bairro { get; set; }

        [JsonProperty(PropertyName = "cidade")]
        public string cidade { get; set; }

        [JsonProperty(PropertyName = "estado")]
        public string estado { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double longitude { get; set; }
    }
}
