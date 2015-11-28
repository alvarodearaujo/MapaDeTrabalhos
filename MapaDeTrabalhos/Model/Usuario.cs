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
    class Usuario
    {
        public string Id{ get; set; }

        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "Senha")]
        public string Senha { get; set; }
    }
}
