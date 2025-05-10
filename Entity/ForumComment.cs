using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ForumComment:IEntity<Guid>
    {
        public Guid ForumPostId { get; set; }
        public Guid? MainComment { get; set; }
        public List<ForumComment>? ChildComments { get; set; }

        [Display(Name = "Comment: ")]
        [StringLength(250)]
        public string Text { get; set; }
        public int Likes { get; set; } = 0;
    }
}
