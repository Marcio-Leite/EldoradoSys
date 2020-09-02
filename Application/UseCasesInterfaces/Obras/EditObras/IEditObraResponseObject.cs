using Application.UseCases.Obras;
using SharedLibrary;

namespace Application.UseCasesInterfaces.Obras.EditObras
{
    public interface IEditObrasResponseObject : IResponse
    {
        ObraResponse ObraResponse { get; }
    }
}
