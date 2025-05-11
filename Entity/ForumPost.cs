using Core.Entities;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//_milestoneService.GetMilestonesByUserIdAsync(userId); {Title,Descriptrion,Date)

namespace Entity
{
    public class ForumPost:IEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? MatchedUserId { get; set; }
        public Guid ForumCategoryId { get; set; }
        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; }

        public Enum_ForumPostType PostType{ get; set; }

        [Display(Name = "Title: ")]
        [StringLength(100, ErrorMessage = "Title can not be longer than 100 characters!")]
        public string Title { get; set; }


        [Display(Name = "Description: ")]
        [StringLength(350, ErrorMessage = "Description can not be longer than 350 characters!")]
        public string Description { get; set; }
        public string? AIResponse { get; set; }
    }
}
