using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.Obras.EditObras
{
    public class IEditObras
    {
        Task<IEditObraResponseObject> Handle(IEditObraRequestObject requestObject);
    }
}