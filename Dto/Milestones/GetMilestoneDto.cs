using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Milestones
{
    public class GetMilestoneDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ForumPostId { get; set; }

        public string MilestoneName { get; set; }
        public string GoalDescription { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
