using AIRefactoring.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIRefactoring.Database
{
	public class ApplicationDbContext : DbContext
	{
		private readonly IConfiguration configuration;

		public ApplicationDbContext(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public DbSet<UserSession> UserSessions { get; set; }
		public DbSet<CodeArtifact> CodeArtifacts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserSession>(entity =>
			{
				entity.ToTable("UserSessions");

				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.HasIndex(e => e.GuestIdentifier);
				entity.Property(e => e.GuestIdentifier).IsRequired();

				entity.Property(e => e.Title).HasMaxLength(255).IsRequired();
				entity.Property(e => e.Prompt).IsRequired();
				entity.Property(e => e.CreatedAt).HasColumnType("datetime2").IsRequired();

				entity.HasMany(d => d.CodeArtifacts).WithOne(p => p.UserSession).HasForeignKey(d => d.UserSessionId).OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<CodeArtifact>(entity =>
			{
				entity.ToTable("CodeArtifacts");

				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.Property(e => e.UserSessionId).IsRequired();
				entity.HasOne(p => p.UserSession).WithMany(d => d.CodeArtifacts).HasForeignKey(p => p.UserSessionId).OnDelete(DeleteBehavior.Cascade);

				entity.Property(e => e.OriginalCode).IsRequired();
				entity.Property(e => e.RefactoredCode).IsRequired();
			});
		}
	}
}
