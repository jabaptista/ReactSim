using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReactSim.Domain.Model;

namespace ReactSim.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDbRepository mongoDbRepository;

        public ActivityRepository(IMongoDbRepository mongoDbRepository)
        {
            this.mongoDbRepository = mongoDbRepository;
        }

        public async Task<Activity> GetOrCreateAsync(string activityId)
        {
            if (string.IsNullOrWhiteSpace(activityId))
            {
                throw new ArgumentNullException(nameof(activityId));
            }

            var filter = Builders<dbo.Activity>.Filter.Eq(a => a.ActivityId, activityId);
            var dboActivity = await mongoDbRepository.GetOneAsync(filter).ConfigureAwait(false);

            if (dboActivity == null)
            {
                var newActivity = new dbo.Activity
                {
                    ActivityId = activityId,
                    Status = (int)ActivityStatus.Draft,
                    Participants = new List<string>()
                };

                await mongoDbRepository.AddOneAsync(newActivity).ConfigureAwait(false);
                dboActivity = newActivity;
            }

            var domain = new Activity(dboActivity.ActivityId, (ActivityStatus)dboActivity.Status);
            foreach (var p in dboActivity.Participants ?? Enumerable.Empty<string>())
            {
                domain.AddParticipant(p);
            }

            return domain;
        }

        public async Task SaveAsync(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            var filter = Builders<dbo.Activity>.Filter.Eq(a => a.ActivityId, activity.Id);
            var update = Builders<dbo.Activity>.Update
                .Set(a => a.Status, (int)activity.Status)
                .Set(a => a.Participants, activity.Participants.ToList())
                .Set(a => a.ActivityId, activity.Id);

            await mongoDbRepository.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }).ConfigureAwait(false);
        }
    }
}
