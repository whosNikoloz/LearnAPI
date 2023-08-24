using LearnAPI.Model.Social;
using LearnAPI.Model.User;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Data
{
    public class LearnDbContext : DbContext
    {
        public LearnDbContext(DbContextOptions options) : base(options)
        {

        }

        //user

        public DbSet<UserModel> Users { get; set; }


        //Social
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }

    }
}
