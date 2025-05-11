using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Milestones
{
    public class UpdateMilestoneDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ForumPostId { get; set; }

        public string MilestoneName { get; set; }
        public string GoalDescription { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime UpdatedDate { get; set; }
    }
}
