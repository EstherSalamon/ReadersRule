using Microsoft.EntityFrameworkCore;

namespace ReadersRule.Data;

public class LibraryDataContext : DbContext
{
    private readonly string _connectionString;

    public LibraryDataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BookTags> BookTags { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<UserPreferences> UserPreferences { get; set; }
}