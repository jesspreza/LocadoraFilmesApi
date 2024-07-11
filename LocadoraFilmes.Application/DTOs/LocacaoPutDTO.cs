using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Application.DTOs
{
    public class LocacaoPutDTO
    {
        [Required(ErrorMessage = "É obrigatório informar o id da locação")]
        [Range(1, int.MaxValue, ErrorMessage = "O id da locação é inválido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a data da devolução")]
        public DateTime DataDevolucao { get; set; }

        [Required(ErrorMessage = "É obrigatório informar se foi entregue ou não")]
        public bool Entregue { get; set; }
    }
}
