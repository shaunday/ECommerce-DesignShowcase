using System.Threading.Tasks;
using Core;

namespace Adapters.WebApi.ApiGateway
{
    public interface IApiGateway
    {
        Task<object?> RouteRequestAsync(string route, WorkflowContext context);
    }
} 