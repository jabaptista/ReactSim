using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactSim.Repositories.dbo
{

    public abstract class MongoEntity
    {
        [BsonId]
        public BsonValue Id { get; set; }

        public ObjectId? GetObjectId()
        {
            if (Id == null) return null;
            if (Id is BsonObjectId boid) return boid.Value;
            if (Id.IsObjectId) return Id.AsObjectId;
            return null;
        }

        public int? GetInt32Id()
        {
            if (Id == null) return null;
            if (Id is BsonInt32 bi) return bi.Value;
            if (Id.IsInt32) return Id.AsInt32;
            return null;
        }

        public void SetObjectId(ObjectId id) => Id = new BsonObjectId(id);

        public void SetInt32Id(int id) => Id = new BsonInt32(id);
    }
}
