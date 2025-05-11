using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ForumPostMatchSubmitUser : IEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ForumPostId { get; set; }
    }
}
