using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserProfession:IEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ForumPostId { get; set; }
        public string ProfessionName { get; set; }
        public string ProfessionDescription { get; set; }
        public bool IsActive { get; set; }
        public int MilestonesAchieved { get; set; } = 0;
        public int SkillLevel { get; set; } = 1; //1 = Beginner, 2= Intermediate, 3= Advanced, 4=Professional
    }
}
