﻿using Fiap03.MOD;
using Fiap03.MOD.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Models
{
    public class CarroModel
    {
        public int Id { get; set; }
        //FK
        [Required]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        [Range(minimum:1950, maximum: 3000)]
        public int Ano { get; set; }

        public bool Esportivo { get; set; }

        [Required, StringLength(maximumLength:8,MinimumLength = 8)]
        public string Placa { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Combustível")]
        public Combustivel? Combustivel { get; set; }

        //FK         
        public DocumentoModel Documento { get; set; }
        public int Renavam { get; set; }

        //Construtores
        public CarroModel(CarroMOD mod)
        {
            Id = mod.Id;
            MarcaId = mod.MarcaId;
            Ano = mod.Ano;
            Combustivel = mod.Combustivel;
            Esportivo = mod.Esportivo;
            Placa = mod.Placa;
            Descricao = mod.Descricao;
            Renavam = mod.Renavam;
            if (mod.Documento != null)
                Documento = new DocumentoModel(mod.Documento);
        }

        public CarroModel()
        {

        }
        
    }
}