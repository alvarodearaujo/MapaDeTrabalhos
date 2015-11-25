﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTrabalhos.Model
{
    class Pessoa
    {
        public int Id { get; set; }

        public string nomeOuRazaoSocial { get; set; }

        public string cnpj { get; set; }

        public string telefone { get; set; }

        public string email { get; set; }

        public string celular { get; set; }

        public Boolean isPessoaFisica { get; set; }

    }
}