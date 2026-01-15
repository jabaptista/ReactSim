using System;
using System.Threading.Tasks;
using ReactSim.Domain.Model;
using ReactSim.Repositories;

namespace ReactSim.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository repository;

        public ActivityService(IActivityRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Activity> DeployAsync(string activityId)
        {
            if (string.IsNullOrWhiteSpace(activityId))
            {
                throw new ArgumentNullException(nameof(activityId));
            }

            var activity = await repository.GetOrCreateAsync(activityId).ConfigureAwait(false);
            activity.SetStatus(ActivityStatus.Deployable);
            await repository.SaveAsync(activity).ConfigureAwait(false);
            return activity;
        }

        public async Task<Activity> RegisterStartAsync(string activityId, string participantId)
        {
            if (string.IsNullOrWhiteSpace(activityId))
            {
                throw new ArgumentNullException(nameof(activityId));
            }

            var activity = await repository.GetOrCreateAsync(activityId).ConfigureAwait(false);
            activity.AddParticipant(participantId);
            await repository.SaveAsync(activity).ConfigureAwait(false);
            return activity;
        }
    }
}
