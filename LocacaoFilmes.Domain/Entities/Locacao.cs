using LocacaoFilmes.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LocadoraFilmes.Domain.Entities
{
    public partial class Locacao
    {
        [Key]
        public int Id { get; set; }

        // Chave estrangeira para o cliente
        public int ClienteId { get; private set; }

        public virtual Cliente Cliente { get; set; }

        public DateTime DataLocacao { get; private set; }

        public DateTime DataDevolucao { get; private set; }

        public bool Entregue { get; private set; }

        // Relacionamento muitos para muitos com filmes através de uma tabela intermediária
        public virtual ICollection<LocacaoFilme> LocacaoFilmes { get; set; }

        [NotMapped]
        public virtual List<int> FilmesId { get; set; }

        public Locacao(int id, int clienteId, DateTime dataLocacao, DateTime dataDevolucao, bool entregue)
        {
            DomainExceptionValidation.When(id < 0, "O Id da locação deve ser positivo.");

            Id = id;
            Entregue = entregue;
            ValidateCliente(clienteId);
            ValidateDatas(dataLocacao, dataDevolucao);
        }

        public Locacao(int clienteId, DateTime dataLocacao, DateTime dataDevolucao, bool entregue)
        {
            Entregue = entregue;
            ValidateCliente(clienteId);
            ValidateDatas(dataLocacao, dataDevolucao);
        }

        public void Update(DateTime dataLocacao, DateTime dataDevolucao, bool entregue)
        {
            Entregue = entregue;
            ValidateDatas(dataLocacao, dataDevolucao);
        }

        public void ValidateCliente(int clienteId)
        {
            DomainExceptionValidation.When(clienteId < 0, "O Id do cliente deve ser positivo.");

            ClienteId = clienteId;
            
        }

        public void ValidateDatas(DateTime dataLocacao, DateTime dataDevolucao)
        {
            DomainExceptionValidation.When(dataLocacao.Date > dataDevolucao.Date, "A data de devolução não pode ser anterior à data de locação");

            DataLocacao = dataLocacao;
            DataDevolucao = dataDevolucao;
        }
    }
}
