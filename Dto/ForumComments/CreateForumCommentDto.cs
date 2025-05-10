using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ForumComments
{
    public class CreateForumCommentDto
    {
        public Guid ForumPostId { get; set; }
        public Guid? MainComment { get; set; }
        public List<ForumComment>? ChildComments { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
