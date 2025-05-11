using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ForumPosts
{
    public class SubmitForumMatchDto
    {
        public Guid UserId { get; set; }
        public Guid ForumPostId { get; set; }
    }
}
