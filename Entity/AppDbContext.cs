using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entity
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<ForumPostMatchSubmitUser> ForumPostMatchSubmitUsers { get; set; }
        public DbSet<UserProfession> UserProfessions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional configuration if needed
        }
    }
}
