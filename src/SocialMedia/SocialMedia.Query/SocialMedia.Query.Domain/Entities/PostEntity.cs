using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Query.Domain.Entities
{
    [Table("POSTS")]
    public class PostEntity : BaseEntity
    {
        public string Author { get; set; }
        public string Message { get; set; }
        public int Likes { get; set; }
        public bool Edited { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
    }
}