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
    
    public DbSet<Item> Items { get; set; }
    
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
        
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Character)
            .WithMany(c => c.Item)
            .HasForeignKey(i => i.CharacterId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Item>()
            .HasOne(i => i.Quest)
            .WithMany(q => q.RewardPool)
            .HasForeignKey(i => i.QuestId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}