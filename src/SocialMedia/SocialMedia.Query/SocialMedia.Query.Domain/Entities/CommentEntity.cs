using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Query.Domain.Entities
{
    [Table("COMMENTS")]
    public class CommentEntity : BaseEntity
    {
        public Guid PostId { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public bool Edited { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual PostEntity Post { get; set; }
    }
}