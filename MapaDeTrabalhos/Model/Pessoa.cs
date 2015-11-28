using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapaDeTrabalhos.Model
{
    class Pessoa
    {
        public string Id { get; set; }


        [JsonProperty(PropertyName = "nome")]
        public string nomeOuRazaoSocial { get; set; }

        [JsonProperty(PropertyName = "cpfOuCnpj")]
        public string cpfOuCnpj { get; set; }

        [JsonProperty(PropertyName = "telefone")]
        public string telefone { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }

        [JsonProperty(PropertyName = "celular")]
        public string celular { get; set; }

        [JsonProperty(PropertyName = "isPessoaFisica")]
        public Boolean isPessoaFisica { get; set; }

        [JsonProperty(PropertyName = "endereco")]
        public Endereco endereco { get; set; }

        [JsonProperty(PropertyName = "usuario")]
        public Usuario usuario { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double longitude { get; set; }


    }
}
