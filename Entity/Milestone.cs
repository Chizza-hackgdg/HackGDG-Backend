using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Milestone:IEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ForumPostId { get; set; }

        [Display(Name = "Milestone Name: ")]
        [StringLength(100, ErrorMessage = "Milestone name can not be longer than 100 characters!")]
        public string MilestoneName { get; set; }

        [Display(Name = "Goal Description: ")]
        [StringLength(350, ErrorMessage = "Goal description can not be longer than 350 characters!")]
        public string GoalDescription { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
