using Application.UseCases.Obras;
using SharedLibrary;

namespace Application.UseCasesInterfaces.Obras.AddObras
{
    public interface IAddObraResponseObject : IResponse
    {
        ObraResponse ObraResponse { get; }
    }
}