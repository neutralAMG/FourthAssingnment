
using ForthAssignment.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
			optionsBuilder.UseSqlServer("Server=DESKTOP-LL4GL68; Database=fourthAssingnment; Integrated Security=true; TrustServerCertificate=true;", b => b.MigrationsAssembly("ForthAssignment.Infraestructure.Persistence"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>( u =>
			{

			});

			modelBuilder.Entity<UserFriend>(u =>
			{

			});

			modelBuilder.Entity<Post>(p =>
			{

			});

			modelBuilder.Entity<Comment>(c =>
			{

			});
		}
	}
}
