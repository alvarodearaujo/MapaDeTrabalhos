using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mapadetrabalhomobileserviceService.DataObjects
{
    public class Idioma : EntityData
    {
        public string CurriculoId { get; set; }

        [ForeignKey("CurriculoId")]
        public virtual Curriculo curriculo { get; set; }

        public string nomeIdioma { get; set; }

        public string nivelIdioma { get; set; }
    }
}