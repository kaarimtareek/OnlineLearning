using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<LookupUserStatus> LookupUserStatuses { get; set; }
        public DbSet<LookupUserRoomStatus> LookupUserRoomStatuses { get; set; }
        public DbSet<LookupRoomMeetingStatus> LookupRoomMeetingStatuses { get; set; }
        public DbSet<LookupRoomStatus> LookupRoomStatuses { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UsersRooms> UsersRooms { get; set; }
        public DbSet<RoomInterest> RoomInterests { get; set; }
        public DbSet<RoomRoadMap> RoomRoadMaps { get; set; }
        public DbSet<RoomMaterial> RoomMaterials { get; set; }
        public DbSet<RoomMeeting> RoomMeetings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasOne(x => x.User).WithMany(s => s.Answers).OnDelete(DeleteBehavior.NoAction);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                // for entities that inherit from BaseEntity,
                // set UpdatedOn / CreatedOn appropriately
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable.UpdatedAt = utcNow;

                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedAt").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable.CreatedAt = utcNow;
                            trackable.UpdatedAt = utcNow;
                            break;
                    }
                    if (entry.Entity is ApplicationUser anothertrackable)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                // set the updated date to "now"
                                anothertrackable.UpdatedAt = utcNow;

                                // mark property as "don't touch"
                                // we don't want to update on a Modify operation
                                entry.Property("CreatedAt").IsModified = false;
                                break;

                            case EntityState.Added:
                                // set both updated and created date to "now"
                                anothertrackable.CreatedAt = utcNow;
                                anothertrackable.UpdatedAt = utcNow;
                                break;
                        }
                    }
                }
            }
        }
    }
}