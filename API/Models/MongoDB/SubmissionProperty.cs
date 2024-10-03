using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Options;

namespace SubmissionsProcessor.API.Models.MongoDB
{
    public class SubmissionProperty
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SubmissionId { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.Document)]
        public List<Dictionary<string, string>> Properties { get; set; }

    }
}
