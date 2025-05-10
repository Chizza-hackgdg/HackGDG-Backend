using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ForumComments
{
    public class ForumCommentDto
    {
        public Guid Id { get; set; }
        public Guid ForumPostId { get; set; }
        public Guid? MainComment { get; set; }
        public List<ForumComment>? ChildComments { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate{ get; set; }
        public bool IsDeleted { get; set; }
    }
}
