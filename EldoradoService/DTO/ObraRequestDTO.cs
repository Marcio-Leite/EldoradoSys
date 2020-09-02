using System;
using Domain;

namespace EldoradoService.DTO
{
    public class ObraRequestDTO
    {
        public string DescricaoObra { get; set; }
        public DateTime DataInicioObras { get; set; }
        public DateTime DataEntregaEmpreendimento { get; set; }
        public DateTime DataValidadeGarantia { get; set; }
        public decimal ValorTotalAquisicao { get; set; }
        public decimal PorcentagemParticipacao { get; set; }
        public Status StatusObra { get; set; }
    }
}