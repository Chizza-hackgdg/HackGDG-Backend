using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ForumPosts
{
   public class CreateForumPostDto
    {
        public Guid UserId { get; set; }
        public Guid? MatchedUserId { get; set; }
        public Guid ForumCategoryId { get; set; }
        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; }
        public Enum_ForumPostType PostType { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string? AIResponse { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
