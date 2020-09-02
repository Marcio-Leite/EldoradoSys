using System;
using SharedLibrary;

namespace Application.UseCasesInterfaces.Obras.EditObras
{
    public interface IEditObraRequestObject : IValidator
    {
        public string DescricaoObra { get; }
        public DateTime DataInicioObras { get; }

        public DateTime DataEntregaEmpreendimento { get; }

        public DateTime DataValidadeGarantia { get; }

        public Decimal ValorTotalAquisicao { get; }

        public Decimal PorcentagemParticipacao { get; }
    }
}