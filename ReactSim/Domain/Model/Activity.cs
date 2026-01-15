using System.Collections.Generic;

namespace ReactSim.Domain.Model
{
    public enum ActivityStatus
    {
        Draft = 0,
        Deployable = 1,
        Terminated = 2
    }

    public class Activity
    {
        public string Id { get; }
        public ActivityStatus Status { get; private set; }
        public HashSet<string> Participants { get; }

        public Activity(string id, ActivityStatus status)
        {
            Id = id;
            Status = status;
            Participants = new HashSet<string>();
        }

        public void SetStatus(ActivityStatus status) => Status = status;

        public void AddParticipant(string participantId)
        {
            if (!string.IsNullOrWhiteSpace(participantId))
            {
                Participants.Add(participantId);
            }
        }
    }
}
