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
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pessoa")]
        public Pessoa pessoa { get; set; }

        public string Rua { get; set; }

        public string numero { get; set; }

        public string bairro { get; set; }

        public string cidade { get; set; }

        public string estado { get; set; }
    }
}
