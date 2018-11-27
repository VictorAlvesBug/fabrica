using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Models
{
    /**
     * Criar a tabela e realizar CRUD
     * */
    public class MarcaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório"), StringLength(50)]
        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }

        [Required,StringLength(18, MinimumLength = 18)]
        public string Cnpj { get; set; }

        public IList<CarroModel> Carros { get; set; }

        public MarcaModel(MarcaMOD mod)
        {
            Nome = mod.Nome;
            Id = mod.Id;
            DataCriacao = mod.DataCriacao;
            Cnpj = mod.Cnpj;
            if (mod.Carros != null)
            {
                //instancia a lista de carroModel
                var lista = new List<CarroModel>();
                //Popula a lista com os carros
                mod.Carros.ToList().ForEach(c =>
                     lista.Add(new CarroModel(c)));
                //Associa a lista na propriedade
                Carros = lista;
            }
        }

        public MarcaModel() { }

    }
}