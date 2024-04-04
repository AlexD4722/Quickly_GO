using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Models;
using QuicklyGo.Unit;

namespace QuicklyGo.Data
{
    public class QuicklyGoDbContext : AuditableDbContext
    {
        public QuicklyGoDbContext(DbContextOptions<QuicklyGoDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Message> Messages { get; set; }
        public DbSet<ReadMessage> MessageReceipients { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<Notice> Notices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>()
                .Property(u => u.Id).HasMaxLength(21);
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.UserName).IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber).IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber).IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Role).HasDefaultValue(Roles.User);
            modelBuilder.Entity<User>()
                .Property(u => u.VerifyCode).HasMaxLength(8);
            modelBuilder.Entity<User>()
                .Property(u => u.UrlImgAvatar).HasColumnType("text");
            modelBuilder.Entity<User>()
             .Property(u => u.UrlBackground).HasColumnType("text");
            modelBuilder.Entity<User>()
                .Property(u => u.Status).HasDefaultValue(UserStatus.Pending);
            modelBuilder.Entity<User>()
                .Property(u => u.Description).IsRequired(false);
            modelBuilder.Entity<User>()
                .Property(u => u.Location).IsRequired(false);
            modelBuilder.Entity<User>()
                .Property(u => u.UrlImgAvatar)
                .HasDefaultValue("Img/avatar/User-Default.jpg");
            modelBuilder.Entity<User>()
               .Property(u => u.UrlBackground)
               .HasDefaultValue("Img/avatar_background/default.jpg");
            // Message
            modelBuilder.Entity<Models.Message>()
                .Property(m => m.CreatorId).IsRequired();
            modelBuilder.Entity<Models.Message>()
                .Property(m => m.ConversationId).IsRequired();
            modelBuilder.Entity<Models.Message>()
                .Property(m => m.BodyContent).IsRequired().IsUnicode();
            modelBuilder.Entity<Models.Message>()
                .Property(u => u.Status).HasDefaultValue(MessageStatus.Active);

            // ReadMessage
            modelBuilder.Entity<ReadMessage>()
                .Property(mr => mr.MessageId).IsRequired();
            modelBuilder.Entity<ReadMessage>()
                .Property(mr => mr.ReceipientId).IsRequired();

            // Relationship
            modelBuilder.Entity<Relationship>()
                .Property(r => r.UserId).IsRequired();
            modelBuilder.Entity<Relationship>()
                .Property(r => r.FriendId).IsRequired();
            modelBuilder.Entity<Relationship>()
                .Property(r => r.Status).HasDefaultValue(RelationshipStatus.Active);

            // Conversation
            modelBuilder.Entity<Conversation>()
                .Property(c => c.Id).HasMaxLength(21);
            modelBuilder.Entity<Conversation>()
                .Property(c => c.UrlImg).IsRequired();
            modelBuilder.Entity<Conversation>()
                .Property(c => c.UrlImg).HasDefaultValue("Img/avatar/User-Default.jpg");
            modelBuilder.Entity<Conversation>()
                .Property(c => c.UrlImg).HasColumnType("text");
            modelBuilder.Entity<Conversation>()
                .Property(c => c.Id).IsRequired();
            modelBuilder.Entity<Conversation>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Conversation>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Conversation>()
                .Property(c => c.Description).HasColumnType("text");
            modelBuilder.Entity<Conversation>()
                .Property(c => c.Group).HasDefaultValue(false);
            modelBuilder.Entity<Conversation>()
                .Property(c => c.Status).HasDefaultValue(ConversationStatus.Active);

            // UserConversation
            modelBuilder.Entity<UserConversation>()
                .HasKey(uc => uc.Id);
            modelBuilder.Entity<UserConversation>()
                .Property(uc => uc.UserId).IsRequired();
            modelBuilder.Entity<UserConversation>()
                .Property(uc => uc.ConversationId).IsRequired();
            modelBuilder.Entity<UserConversation>()
                .Property(uc => uc.LastSeenMessage).IsRequired(false);
            modelBuilder.Entity<UserConversation>()
                .Property(uc => uc.Status).HasDefaultValue(UserConversationStatus.Active);

            // Relationship
            modelBuilder.Entity<Relationship>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Relationship>()
                .Property(r => r.UserId).IsRequired();
            modelBuilder.Entity<Relationship>()
                .Property(r => r.FriendId).IsRequired();
            modelBuilder.Entity<Relationship>()
                .Property(r => r.Alias).IsRequired(false);
            modelBuilder.Entity<Relationship>()
                .Property(r => r.Status).HasDefaultValue(RelationshipStatus.Active);

            // Notice
            modelBuilder.Entity<Notice>()
                .HasKey(n => n.Id);
            modelBuilder.Entity<Notice>()
                .Property(n => n.Title).IsRequired();
            modelBuilder.Entity<Notice>()
                .Property(n => n.UrlImg).IsRequired(false);
            modelBuilder.Entity<Notice>()
                .Property(n => n.Description).IsRequired(false);
            modelBuilder.Entity<Notice>()
                .Property(n => n.Status).HasDefaultValue(NoticeStatus.NotRead);

            // Relation between tables
            modelBuilder.Entity<UserConversation>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserConversations)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserConversation>()
                .HasOne(uc => uc.Conversation)
                .WithMany(c => c.UserConversations)
                .HasForeignKey(uc => uc.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReadMessage>()
                .HasOne(mr => mr.Message)
                .WithMany(m => m.ReadMessages)
                .HasForeignKey(mr => mr.MessageId)
                .OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<ReadMessage>()
                .HasOne(mr => mr.Receipient)
                .WithMany(u => u.ReadMessages)
                .HasForeignKey(mr => mr.ReceipientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.User)
                .WithMany(u => u.Relationships)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.Friend)
                .WithMany(u => u.FriendRelationships)
                .HasForeignKey(r => r.FriendId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Models.Notice>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notices)
                .HasForeignKey(n => n.UserId);
            modelBuilder.Entity<Models.User>()
                .HasMany(u => u.Notices)
                .WithOne(n => n.User);

            // Seed data
            var randomId1 = CreateGenerateUniqueKey.GenerateUniqueKey();
            var randomId2 = CreateGenerateUniqueKey.GenerateUniqueKey();
            modelBuilder.Entity<User>().HasData(
                               new User
                               {
                                   Id = randomId1,
                                   FirstName = "Admin",
                                   LastName = "Admin",
                                   UserName = "admin",
                                   Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                                   PhoneNumber = "0123456789",
                                   VerifyCode = "123456",
                                   Email = "admin@gmail.com",
                                   Status = UserStatus.Active,
                                   CreateAt = DateTime.Now,
                                   UpdateAt = DateTime.Now, 
                                   Role = Roles.Admin
                               });
            modelBuilder.Entity<User>().HasData(
                               new User
                               {
                                   Id = randomId2,
                                   FirstName = "Lam",
                                   LastName = "Tran",
                                   UserName = "lamtran",
                                   Password = BCrypt.Net.BCrypt.HashPassword("lamtran"),
                                   PhoneNumber = "0123456781",
                                   VerifyCode = "123456",
                                   Email = "lamtran@gmail.com",
                                   Status = UserStatus.Active,
                                   CreateAt = DateTime.Now,
                                   UpdateAt = DateTime.Now,
                                   Role = Roles.Admin
                               });
            modelBuilder.Entity<User>().HasData(
                               new User
                               {
                                   Id = CreateGenerateUniqueKey.GenerateUniqueKey(),
                                   FirstName = "Dung",
                                   LastName = "Nguyen",
                                   UserName = "dungnguyen",
                                   Password = BCrypt.Net.BCrypt.HashPassword("dungnguyen"),
                                   PhoneNumber = "0123456782",
                                   VerifyCode = "123456",
                                   Email = "dungnguyen@gmail.com",
                                   Status = UserStatus.Active,
                                   CreateAt = DateTime.Now,
                                   UpdateAt = DateTime.Now
                               });
            modelBuilder.Entity<User>().HasData(
                               new User
                               {
                                   Id = CreateGenerateUniqueKey.GenerateUniqueKey(),
                                   FirstName = "Vinh",
                                   LastName = "Nguyen",
                                   UserName = "vinhnguyen",
                                   Password = BCrypt.Net.BCrypt.HashPassword("vinhnguyen"),
                                   PhoneNumber = "0123456783",
                                   VerifyCode = "123456",
                                   Email = "vinhnguyen@gmail.com",
                                   Status = UserStatus.Active,
                                   CreateAt = DateTime.Now,
                                   UpdateAt = DateTime.Now
                               });
            modelBuilder.Entity<User>().HasData(
                               new User
                               {
                                   Id = CreateGenerateUniqueKey.GenerateUniqueKey(),
                                   FirstName = "Brad",
                                   LastName = "Pitt",
                                   UserName = "bradpitt",
                                   Password = BCrypt.Net.BCrypt.HashPassword("bradpitt"),
                                   PhoneNumber = "0123456784",
                                   VerifyCode = "123456",
                                   Email = "bradpitt@gmail.com",
                                   Status = UserStatus.Active,
                                   CreateAt = DateTime.Now,
                                   UpdateAt = DateTime.Now
                               });
            modelBuilder.Entity<User>().HasData(
                               new User
                               {
                                   Id = CreateGenerateUniqueKey.GenerateUniqueKey(),
                                   FirstName = "Angelina",
                                   LastName = "Jolie",
                                   UserName = "angelinajolie",
                                   Password = BCrypt.Net.BCrypt.HashPassword("angelinajolie"),
                                   PhoneNumber = "0123456785",
                                   VerifyCode = "123456",
                                   Email = "angelinajolie@gmail.com",
                                   Status = UserStatus.Active,
                                   CreateAt = DateTime.Now,
                                   UpdateAt = DateTime.Now
                               });
            var conversationId1 = CreateGenerateUniqueKey.GenerateUniqueKey();
            modelBuilder.Entity<Conversation>().HasData(
                                              new Conversation
                                              {
                                                  Id = conversationId1,
                                                  Name = "Admin",
                                                  Description = "Admin conversation",
                                                  Group = false,
                                                  Status = ConversationStatus.Active,
                                                  CreateAt = DateTime.Now,
                                                  UpdateAt = DateTime.Now
                                              });
            modelBuilder.Entity<Models.Message>().HasData(
                    new Models.Message
                    {
                        Id = 1,
                        CreatorId = randomId1,
                        ConversationId = conversationId1,
                        BodyContent = "Hello",
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    });
            modelBuilder.Entity<Models.Message>().HasData(
                    new Models.Message
                    {
                        Id = 2,
                        CreatorId = randomId1,
                        ConversationId = conversationId1,
                        BodyContent = "Hi",
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    });
            modelBuilder.Entity<Models.Message>().HasData(
                    new Models.Message
                    {
                        Id = 3,
                        CreatorId = randomId2,
                        ConversationId = conversationId1,
                        BodyContent = "what are you doing ?",
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    });
            modelBuilder.Entity<UserConversation>().HasData(
                    new UserConversation
                    {
                        Id = 1,
                        UserId = randomId1,
                        ConversationId = conversationId1,
                        LastSeenMessage = DateTime.Now,
                        Status = UserConversationStatus.Active,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    });
            modelBuilder.Entity<UserConversation>().HasData(
                    new UserConversation
                    {
                        Id = 2,
                        UserId = randomId2,
                        ConversationId = conversationId1,
                        LastSeenMessage = DateTime.Now,
                        Status = UserConversationStatus.Active,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    });
            modelBuilder.Entity<Notice>()
                .HasData(new Notice
                {
                    Id = 1,
                    Title = "Notice 1",
                    Description = "Notice 1",
                    Status = NoticeStatus.NotRead,
                    UserId = randomId1,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                });
        }
    }
}
