﻿using System;
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
       
        public int Id { get; set; }
      
        public string PessoaId{ get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}