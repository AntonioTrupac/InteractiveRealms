using Microsoft.EntityFrameworkCore;
using Practice.Models;

namespace Practice.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Character> Characters { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<CharacterQuest> CharacterQuests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterQuest>()
            .HasKey(cq => new {cq.CharacterId, cq.QuestId});

        modelBuilder.Entity<CharacterQuest>()
            .HasOne(cq => cq.Character)
            .WithMany(c => c.CharacterQuests)
            .HasForeignKey(cq => cq.CharacterId);
        
        modelBuilder.Entity<CharacterQuest>()
            .HasOne(cq => cq.Quest)
            .WithMany(q => q.CharacterQuests)
            .HasForeignKey(cq => cq.QuestId);

    }
}