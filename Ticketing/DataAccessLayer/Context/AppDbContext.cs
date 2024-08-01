using Microsoft.EntityFrameworkCore;
using Ticketing.DataAccessLayer.Entities;

namespace Ticketing.DataAccessLayer.Context;

public class AppDbContext: DbContext
{
    private readonly IConfiguration _config;

    public AppDbContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupporterRating>()
            .HasKey(sr => new { sr.RatedUserId, sr.RelatedTicketId, sr.SupporterId });

        modelBuilder.Entity<Message>()
            .HasMany(m => m.Replies)
            .WithOne(m => m.ParentMessage)
            .HasForeignKey(m => m.ParentMessageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderId);

        // One-to-Many: User (Creator) -> Tickets
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Creator)
            .WithMany(u => u.CreatedTickets)
            .HasForeignKey(t => t.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Many-to-Many: Tickets <-> Supporters
        // modelBuilder.Entity<Ticket>()
        //     .HasMany(t => t.Supporters)
        //     .WithMany(u => u.AnsweredTicket)
        //     .UsingEntity<Dictionary<string, object>>(
        //         "TicketSupporter",
        //         j => j.HasOne<User>()
        //             .WithMany()
        //             .HasForeignKey("UserId")
        //             .OnDelete(DeleteBehavior.Cascade),
        //         j => j.HasOne<Ticket>()
        //             .WithMany()
        //             .HasForeignKey("TicketId")
        //             .OnDelete(DeleteBehavior.Cascade),
        //         j =>
        //         {
        //             j.HasKey("TicketId", "UserId");
        //         });

        modelBuilder.Entity<User>()
            .HasMany(u => u.AnsweredTicket)
            .WithMany(t => t.Supporters)
            .UsingEntity<TicketSupporter>();

        modelBuilder.Entity<Ticket>()
            .Property(e => e.TrackingNumber)
            .HasDefaultValueSql("NEWID()");

        base.OnModelCreating(modelBuilder);

    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<SupporterRating> SupporterRatings { get; set; }
    public DbSet<TicketSupporter> TicketSupporter { get; set; }
}