using System.Threading.Tasks;
using ReactSim.Domain.Model;

namespace ReactSim.Services
{
    public interface IActivityService
    {
        Task<Activity> DeployAsync(string activityId);
        Task<Activity> RegisterStartAsync(string activityId, string participantId);
    }
}
