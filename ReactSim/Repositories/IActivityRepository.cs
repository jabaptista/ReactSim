using System.Threading.Tasks;
using ReactSim.Domain.Model;

namespace ReactSim.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetOrCreateAsync(string activityId);
        Task SaveAsync(Activity activity);
    }
}
