using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Guid? MatchedUserId { get; set; }
    public Guid? MatchedForumPostId { get; set; }
    public string? MatchedForumPostTitle { get; set; }
    [StringLength(250)]
    public string Name { get; set; }
    [StringLength(250)]
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public int ExpPoint { get; set; }
    public int Level { get; set; }
    public int MilestonesAchieved { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
}
