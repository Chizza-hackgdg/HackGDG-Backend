using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.UserProfessions
{
    public class GetUserProfessionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid ProfessionForumId { get; set; }
        public string ProfessionName { get; set; }
        public string ProfessionDescription { get; set; }
        public int MilestonesAchieved { get; set; }

        public int SkillLevel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
