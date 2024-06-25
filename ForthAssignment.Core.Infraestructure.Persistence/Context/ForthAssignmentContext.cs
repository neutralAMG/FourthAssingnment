
using ForthAssignment.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ForthAssignment.Infraestructure.Persistence.Context
{
	public class ForthAssignmentContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<UserFriend> UsersFriends { get; set; }
	//	public DbSet<PostLike> PostLikes { get; set; }
	//	public DbSet<CommentLike> CommentLikes { get; set; }
        public ForthAssignmentContext(DbContextOptions<ForthAssignmentContext> options) : base(options)
        {
            
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.ConfigureWarnings(warnings =>
			{
				warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored);
			});
			optionsBuilder.UseSqlServer("Server=DESKTOP-LL4GL68; Database=fourthAssingnment; Integrated Security=true; TrustServerCertificate=true;", b => b.MigrationsAssembly("ForthAssignment.Infraestructure.Persistence"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>( u =>
			{
				u.HasKey(u => u.Id);
				u.HasMany(u => u.UserPosts).WithOne(p => p.UserThatPostThis).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
				u.HasMany(u => u.UserComments).WithOne(c => c.UserThatCommentetThis).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
				u.HasMany(u => u.UserFriends).WithOne(u => u.User).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
				u.HasMany(u => u.FriendsOfthUser).WithOne(u => u.UsersFriend).HasForeignKey(u => u.UserFriendId).OnDelete(DeleteBehavior.Cascade);

				u.Property(u => u.Name).IsRequired();
				u.Property(u => u.Email).IsRequired();
				u.Property(u => u.Password).IsRequired();
				u.Property(u => u.PhoneNumber).IsRequired();
				u.Property(u => u.ProfileImageUrl);
			});

			modelBuilder.Entity<UserFriend>(u =>
			{
                u.HasKey(u => u.Id);
                u.HasOne(u => u.User).WithMany(u => u.UserFriends).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.NoAction);
                u.HasOne(u => u.UsersFriend).WithMany(u => u.FriendsOfthUser).HasForeignKey(u => u.UserFriendId).OnDelete(DeleteBehavior.NoAction);

                u.Property(u => u.UserId).IsRequired();
                u.Property(u => u.UserFriendId).IsRequired();

            });

			modelBuilder.Entity<Post>(p =>
			{
                p.HasKey(p => p.Id);
                p.HasOne(p => p.UserThatPostThis).WithMany(u => u.UserPosts).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
                p.HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(u => u.PostId).OnDelete(DeleteBehavior.Cascade);

                p.Property(p => p.UserId).IsRequired();
                p.Property(p => p.PostText).IsRequired();
                p.Property(p => p.DateCreated).IsRequired();
                p.Property(p => p.PostImgUrl);
               
            });

			modelBuilder.Entity<Comment>(c =>
			{
                c.HasKey(c => c.Id);
                c.HasOne(c => c.UserThatCommentetThis).WithMany(u => u.UserComments).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.NoAction);
                c.HasOne(c => c.Post).WithMany(c => c.Comments).HasForeignKey(u => u.PostId).OnDelete(DeleteBehavior.ClientCascade);


				c.HasOne(c => c.ParentComment).WithMany(c => c.Comments).HasForeignKey(u => u.CommentRespondingTo).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);

                c.Property(c => c.UserId).IsRequired();
                c.Property(c => c.CommentImgUrl);
                c.Property(c => c.PostId).IsRequired();
                c.Property(c => c.CommentText).IsRequired();
                c.Property(c => c.DateCreated).IsRequired();
            });
		}
	}
}
