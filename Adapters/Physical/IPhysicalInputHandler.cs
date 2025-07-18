using System.Threading.Tasks;

namespace Adapters.Physical
{
    public interface IPhysicalInputHandler
    {
        Task OnPhysicalInputAsync(string inputType, object data);
    }
} 