using System.Data;
using System.Data.Entity;
using System.Linq;
namespace JournalClub
{
    public class JournalClubContext : DbContext
    {
        public JournalClubContext() : base("name=JournalClubDb") 
        {
            //this.ContextOptions.LazyLoadingEnabled = true;
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<JournalReferenceRegExp> JournalReferenceRegExps { get; set; }
        public DbSet<PicuAttachment> PicuAttachments { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Presenter> Presenters { get; set; }
        public DbSet<TeachingSession> TeachingSessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //enable cascading deletes
            modelBuilder.Entity<Presenter>().HasMany(p => p.Presentations).WithRequired(s => s.Presenter).WillCascadeOnDelete(true);
            modelBuilder.Entity<Presenter>().HasMany(p => p.PicuAttachments).WithRequired(s => s.Presenter).WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
    public static class ContextExtensions
    {
        public static bool RequiresSave(this JournalClubContext context)
        {
            return context.ChangeTracker.Entries()
                      .Any(e => e.State == EntityState.Added
                             || e.State == EntityState.Deleted
                             || e.State == EntityState.Modified);
        }
    }
}