using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ReactSim.Domain.Model;

namespace ReactSim.Repositories
{
    public class InMemoryActivityRepository : IActivityRepository
    {
        private readonly ConcurrentDictionary<string, Activity> store = new();

        public Task<Activity> GetOrCreateAsync(string activityId)
        {
            if (string.IsNullOrWhiteSpace(activityId))
            {
                throw new ArgumentNullException(nameof(activityId));
            }

            var activity = store.GetOrAdd(activityId, id => new Activity(id, ActivityStatus.Draft));
            return Task.FromResult(activity);
        }

        public Task SaveAsync(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            store[activity.Id] = activity;
            return Task.CompletedTask;
        }
    }
}
